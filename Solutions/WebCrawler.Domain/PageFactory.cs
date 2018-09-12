using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain
{
    /// <summary>
    /// Responsiblity is ToUrl Create a Page object with initialized links
    /// uses PageRequester ToUrl make webrequesy.
    /// </summary>
    public class PageFactory : IPageFactory
    {
        private readonly PageRequester _pageRequester;

        public PageFactory(PageRequester requester)
        {
            _pageRequester = requester;
        }

        public PageFactory()
        {
                _pageRequester = new PageRequester();
        }


        public Page CreatePage(Uri address, int pageDepth, CrawlerSession crawlerSession)
        {
            HtmlDocument htmlDocument;
            Page page;
            try
            {
                htmlDocument = GetHtmlDocumentFromWebAddress(address);
                page = new Page(address, htmlDocument, pageDepth, crawlerSession);

                InitializeLinks(htmlDocument, page);
            }
            catch (Exception)
            {
                Console.WriteLine("Could not create page...."  );
                return null;
                
            }
            
         

            return page;
        }

        private HtmlDocument GetHtmlDocumentFromWebAddress(Uri address)
        {
            HtmlDocument htmlDocument = null; 
            try
            {
                string pageAsHtml = _pageRequester.SendWebRequestForPage(address);
                htmlDocument   = new HtmlDocument();
                htmlDocument.LoadHtml(pageAsHtml);
              
            }
            catch (WebException ex)
            {

                //TODO: log exception
            }
            catch (ProtocolViolationException ex)
            {
                //Todo: Log Exception;
            }
           


            return htmlDocument;
        }

        private void InitializeLinks(HtmlDocument htmlDocument, Page page)
        {
            var htmlNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");

            if (htmlNodes != null && htmlNodes.Count > 0)
            {
                foreach (HtmlNode link in htmlNodes)
                {
                    HtmlAttribute attribute = link.Attributes["href"];
                    var url = attribute.Value;
                    Uri newLink;
                    if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out newLink))
                    {
                        page.AddLink(newLink);
                    }

                }
            }
        }

                

    }
}
