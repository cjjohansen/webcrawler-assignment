using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler.Tasks.CommandHandlers
{
    public class ChangeCrawlerSessionResult 
    {
        public bool Success { get; set; }

        public ChangeCrawlerSessionResult(bool success)
        {
            Success = success;
        }
        public String Message { get; set; }
    }
}
