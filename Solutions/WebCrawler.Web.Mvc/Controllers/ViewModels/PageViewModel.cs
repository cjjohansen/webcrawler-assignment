using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler.Web.Mvc.ViewModels
{
    public class PageViewModel
    {
        public String Url { get; set; }
        public IEnumerable<LinkViewModel> Links { get; set; }
        public int BrokenLinksCount { get; set; }

    }
}
