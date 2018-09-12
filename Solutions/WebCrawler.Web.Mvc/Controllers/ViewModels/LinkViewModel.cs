using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler.Web.Mvc.ViewModels
{
    public class LinkViewModel
    {
        public String Url { get; set; }
        public bool IsBroken { get; set; }
    }
}
