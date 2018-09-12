using HtmlAgilityPack;
using Machine.Specifications;
using System;
using WebCrawler.Domain;
using System.Linq;
using Machine.Fakes;


namespace MSpecTests.WebCrawler.PageSpecs
{
   
    [Subject(typeof(Page), "Adding a Link")] 
    public class when_Adding_a_Link_to_a_Page : WithFakes
    {
        private static Uri fromAddress;
        private static Uri toAddress;
        private static Link link;
        private static Page page;
        private static int linkCount;
        private static Uri startAddress;
        private static int searchDepth;
        private static long maxMemoryConsumption;
        private static long batchSize;
        private static CrawlerSettings crawlerSettings;
        private static CrawlerSession crawlerSession;
      
        


        private Establish context = () =>
            {
                startAddress = fromAddress = new Uri(@"http://localhost.webcravler.com");
                toAddress = new Uri(@"http://localhost.webcravler.com/linkA");
                searchDepth = 3;
                maxMemoryConsumption = 10000;
                batchSize = 100;
                crawlerSettings = new CrawlerSettings( searchDepth, maxMemoryConsumption, batchSize);
                crawlerSession = new CrawlerSession(startAddress.ToString(), crawlerSettings);
       
               
                page = new Page(fromAddress, new HtmlDocument(), 0,crawlerSession);
                linkCount = page.Links.Count();
              
            };

        private Because of = () =>
                     link =  page.AddLink(toAddress);

        It page_Links_count_should_increase_by_one = () =>
            page.Links.Count().ShouldEqual(linkCount + 1);
         
        It page_Address_should_equal_link_From_property = () => 
            page.Address.ShouldEqual(link.FromUrl);
 
        It link_should_have_its_To_property_returning_ToAddress = () => 
            link.ToUrl.ShouldEqual(toAddress); 
    } 

   
}
