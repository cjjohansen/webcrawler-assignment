using MvcContrib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawler.Web.Mvc.ViewModels;

namespace WebCrawler.Web.Mvc.Controllers.ViewModels
{
    public class CrawlerSessionListViewModel
    {
        public IPagination<CrawlerSessionViewModel> CrawlerSessions { get; set; }
    }
}