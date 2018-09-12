using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler.Domain
{
    public class BrokenLinkFoundEventArgs  :EventArgs
    {
        public Link Link { get; set; }

        public BrokenLinkFoundEventArgs()
        {

        }

        public BrokenLinkFoundEventArgs(Link link)
        {
            Link = link;
        }
    }
}
