using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler.Domain
{
    public class PageFoundEventArgs :EventArgs
    {
        public Page Page { get; set; }

        public PageFoundEventArgs()
        {

        }

        public PageFoundEventArgs(Page page)
        {
            Page = page;
        }
    }
}
