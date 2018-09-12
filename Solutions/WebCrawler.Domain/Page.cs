using System.IO;
using HtmlAgilityPack;
using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain
{
    public class Page :Entity
    {
        
        private Uri _address;
        private List<Link> _links;
        private readonly int _pageDepth;
        private CrawlerSession _crawlerSession;
        private String _innerHtml;
        private HtmlDocument _innerHtmlDocument;

        // Event for every new page found
        public virtual event EventHandler<PageFoundEventArgs> PageFound;
        // Event for every new Broken Link found
        public virtual event EventHandler<BrokenLinkFoundEventArgs> BrokenLinkFound;

        private void OnPageFound(Page page)
        {
            EventHandler<PageFoundEventArgs> handler = PageFound;
            if (handler != null)
            {
                handler(this, new PageFoundEventArgs(page));
            }
        }

        private void OnBrokenLinkFound(Link link)
        {
            EventHandler<BrokenLinkFoundEventArgs> handler = BrokenLinkFound;
            if (handler != null)
            {
                handler(this, new BrokenLinkFoundEventArgs(link));
            }
        }

        protected Page()
        {
                
        }

        public Page(Uri address, HtmlDocument innerHtml, int pageDepth, CrawlerSession crawlerSession)
        {
            _address = address;
            _links = new List<Link>();
            _pageDepth = pageDepth;
            _crawlerSession = crawlerSession;
            var textWriter = new StringWriter();
            _innerHtmlDocument = innerHtml;
             innerHtml.Save(textWriter);
            _innerHtml = textWriter.ToString();

        }

      
        [DomainSignature]
        public virtual Uri Address
        {
            get { return _address; }
        }

        public virtual IQueryable<Link> Links
        {
            get { return new EnumerableQuery<Link>(_links); }
        }

        protected virtual string InnerHtml
        {
            get { return _innerHtml; }
            set { _innerHtml = value; }
        }

        public virtual HtmlDocument InnerHtmlDocument
        {
            get
            {
                if (_innerHtmlDocument == null)
                {    _innerHtmlDocument = new HtmlDocument();
                    _innerHtmlDocument.LoadHtml(_innerHtml);               
                }

                return _innerHtmlDocument;
            }
        }

        public virtual Link AddLink(Uri toAddress)
        {
            var newLink = new Link(Address, toAddress);
            if (_links.Contains(newLink))
            {
                return null;
            }
            else
            {
                _links.Add(newLink);
                return newLink;
            }
        }

        public virtual void Crawl()
        {
            if (_pageDepth < _crawlerSession.Settings.SearchDepth)
            {
                var pageFactory = new PageFactory();
                foreach (var link in Links)
                {
                    var page = pageFactory.CreatePage(link.ToUrl, _pageDepth + 1, _crawlerSession);
                    if (page != null && !_crawlerSession.Pages.Contains(page)) //Only Add and Crawl new pages
                    {
                        _crawlerSession.AddPage(page);
                        page.Crawl();
                    }
                    else
                    {
                        _crawlerSession.AddBrokenLink(link);
                        OnBrokenLinkFound(link);
                    }
                }
            }
        }

    }
}
