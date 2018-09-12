using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCrawler.Web.Mvc.ViewModels
{
    public class CrawlerSessionViewModel
    {
        [Required]
        public String Title { get; set; }
        public String DateTime { get; set; }
        [Required]
        public String StartUrl { get; set; }
        public int Id { get; set; }
        public long PageCount { get; set; }
        public IQueryable<PageViewModel> Pages{ get; set; }

        [Required]
        public int SearchDepth { get; set; }

    
    }
}