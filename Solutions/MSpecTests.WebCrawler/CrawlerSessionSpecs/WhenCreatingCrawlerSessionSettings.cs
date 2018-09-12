using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using WebCrawler.Domain;

namespace MSpecTests.WebCrawler.CrawlerSessionSpecs
{
    /// <summary>
    /// Test is actually redundant. We are testing the .net framework here
    /// </summary>
    [Subject(typeof(CrawlerSettings), "When Creating CrawlerSessionSettings")]
    public class When_creating_a_crawlerSettings_instance
    {
        private static Uri startAddress;
        private static int searchDepth;
        private static long maxMemoryConsumption;
        private static long batchSize;
        private static CrawlerSettings crawlerSettings;
        




        private Establish context = () =>
        {
            startAddress = new Uri(@"http://localhost.webcravler.com");
            searchDepth = 3;
            maxMemoryConsumption = 10000;
            batchSize = 100;
           
           

        };

        private Because of = () =>
                      crawlerSettings = new CrawlerSettings( searchDepth, maxMemoryConsumption, batchSize);

       

        It SearchDepth_Should_be_set = () =>
            crawlerSettings.SearchDepth.ShouldEqual(searchDepth);

        It MaxMemoryConsumption_should_be_set = () =>
             crawlerSettings.MaxMemoryConsumption.ShouldEqual(maxMemoryConsumption);

        It BatchSize_should_be_set = () =>
            crawlerSettings.BatchSize.ShouldEqual(batchSize);

      
    } 

}
