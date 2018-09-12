using HtmlAgilityPack;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Domain;
using Machine.Fakes;
using FluentAssertions;

namespace MSpecTests.WebCrawler.CrawlerSessionSpecs
{

    [Subject(typeof(CrawlerSession), "calling Start")]
    public class when_calling_start
    {
        private static CrawlerSession crawlerSession;
        private static CrawlerSettings settings;
        private static int searchDepth;
        private static long maxMemoryConsumption;
        private static long batchSize;
        private static Uri startAddress;
        private static Page firstPage;
      




        private Establish context = () =>
        {
            startAddress = new Uri(@"http://www.google.com");
            searchDepth = 3;
            maxMemoryConsumption = 10000;
            batchSize = 100;
            settings = new CrawlerSettings( searchDepth, maxMemoryConsumption, batchSize);
            crawlerSession = new CrawlerSession(startAddress.ToString(),settings);
            crawlerSession.MonitorEvents();
            firstPage = new Page(startAddress, new HtmlDocument(),0,crawlerSession);


        };

        private Because of = () =>
                     crawlerSession.Start();

        It a_webpage_should_be_found_and_a_page_instance_should_be_created = () =>
            crawlerSession.Pages.Count().ShouldBeGreaterThan(0);

        It PageFound_event_should_be_raised = () =>
            crawlerSession.ShouldRaise("PageFound")
            .WithSender(crawlerSession)
            .WithArgs<PageFoundEventArgs>(args => args.Page == firstPage );
           

        It all_links_on_the_webpage_should_be_found_and_added_to_the_page_instance = () =>
            crawlerSession.StartPage.Links.ShouldNotBeNull();
    } 

}
