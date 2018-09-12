using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpArch.Domain.Commands;

namespace WebCrawler.Tasks.Commands
{
     public class ChangeCrawlerSessionCommand : CommandBase
        {
         public ChangeCrawlerSessionCommand(
                                                int id,
                                                string title,
                                                string startUrl,
                                                int searchDepth
                                               )
         {
             if (id != 0)
                 Id = id;
                this.Title = title;
                this.StartUrl = startUrl;
                this.SearchDepth = searchDepth;

            }

         [Required]
         public int Id { get; set; }

          
            [Required]
            public string StartUrl { get; set; }

            [Required]
            public string Title { get; set; }

            [Required]
            public int SearchDepth { get; set; }
        }
    
}
