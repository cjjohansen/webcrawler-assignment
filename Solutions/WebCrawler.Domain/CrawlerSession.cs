using System.Linq;
using System;
using System.Collections.Generic;
using SharpArch.Domain.DomainModel;


namespace WebCrawler.Domain
{   
    /// <summary>
    /// CrawlerSession represents a crawl session.
    /// The crawler starts at the StartUrl, and finds all links on that page, 
    /// and recursively crawls these links until MaxSearchDepth is reached
    /// </summary>
    public  class CrawlerSession : Entity
    {
        // read only to achieve maximum encapsulation
        private readonly IList<Page> _pages = new List<Page>();
        private readonly IList<Link> _brokenLinks = new List<Link>();

        [DomainSignature] //Will be used by base class equals and GetHashCode implementation to determine uniqueness of the entity
        public virtual String StartUrl
        {
            get { return _startUrl; }
            protected set { _startUrl = value; }
        }

        public virtual IEnumerable<Page> Pages
        {
            get { return _pages.Select(page => page); }
           
        }

      
        public virtual IEnumerable<Link> BrokenLinks
        {
            get { return _brokenLinks.Select(link => link); }
           
        }

        /// <summary>
        /// Time of last crawler session invocation. (More intention revealing  name could be used here)
        /// </summary>
        public virtual DateTime? DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        public virtual String Title { get; set; }
        private CrawlerSettings _settings;
      
        //TODO: Add event for broken link found
        private Page _startPage;
        private readonly PageRequester _pageRequester;
        private DateTime? _dateTime;
        private string _startUrl;

        // Event for every new page found
        public virtual event EventHandler<PageFoundEventArgs> PageFound;
        // Event for every new Broken Link found
        public virtual event EventHandler<BrokenLinkFoundEventArgs> BrokenLinkFound;

        private void OnPageFound(Page page)
        {
            EventHandler<PageFoundEventArgs> handler = PageFound;
            if (handler != null)
            {
                handler(this, new PageFoundEventArgs( page));
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

        public CrawlerSession()
        {
            _pageRequester = new PageRequester();
            _settings = new CrawlerSettings(3,1000000,1);
            _pages = new List<Page>();
            _brokenLinks = new List<Link>();
            _dateTime = null;


        }

        public CrawlerSession(String startUrl,CrawlerSettings settings):this()
        {
            
            _settings = settings;
            _startUrl = startUrl;
        }

        /// <summary>
        /// Lazy initialization of Start page object
        /// </summary>
        public virtual Page StartPage
        {
            get
            {
                if (_startPage == null)
                {
                    _startPage = new PageFactory(_pageRequester).CreatePage(new Uri(StartUrl), 0, this);
                   if(!_pages.Contains(_startPage))
                        _pages.Add(_startPage);
                }
                return _startPage;
            }

        }

        public virtual CrawlerSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }


        public virtual void Start()
        {
            StartPage.Crawl();
        }



        /// <summary>
        /// Add page to session page collection
        /// </summary>
        /// <param name="page"></param>
        public virtual void  AddPage(Page page)
        {
            if (!_pages.Contains(page))
            {
                _pages.Add(page);
                OnPageFound(page);
            }
        }




        /// <summary>
        /// Update Session method supplies one single way of modifying the Session settings
        /// </summary>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="searchDepth"></param>
        public virtual void ChangeSession(string title, string url, int searchDepth)
        {
            Title = title;
            StartUrl = url;
            // _settings is a value object so we create a new one every time we change anything.
            _settings = new CrawlerSettings(searchDepth, _settings.MaxMemoryConsumption, _settings.BatchSize);
             
        }

        internal void AddBrokenLink(Link link)
        {
            if (!_brokenLinks.Contains(link))
            {
                _brokenLinks.Add(link);
                OnBrokenLinkFound(link);
            }
        }
    }
}
