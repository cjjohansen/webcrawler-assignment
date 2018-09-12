using MvcContrib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCrawler.Web.Mvc.ViewModels;

namespace WebCrawler.Web.Mvc.Controllers.Queries
{
    public interface ICrawlerSessionListQuery
    {
        IPagination<CrawlerSessionViewModel> GetPagedList(int page, int size);
    }
}
