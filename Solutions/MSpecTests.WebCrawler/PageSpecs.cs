using Machine.Specifications;
using System;
using WebCrawler.Domain;


namespace MSpecTests.WebCrawler
{
   
    [Subject(typeof(Page), "Adding a Link")] 
    public class when_Adding_a_Link_to_a_Page
    {
        private static Uri fromAddress;
        private static Uri toAddress;
        private static Link link;
        private static Page page;
        private static int linkCount;
      
        


        private Establish context = () =>
            {
                fromAddress = new Uri(@"http://localhost.webcravler.com");
                toAddress = new Uri(@"http://localhost.webcravler.com/linkA");
                page = new Page(fromAddress);
                linkCount = page.Links..Count;
              
            };

        private Because of = () =>
                     link =  page.AddLink(toAddress);

        It page_Links_count_should_increase_by_one = () =>
                                                                 page.Links.Count.ShouldEqual(linkCount + 1);
         
        It page_Address_should_equal_link_From_property = () => 
            page.Address.ShouldEqual(link.From);
 
        It link_should_have_its_To_property_returning_ToAddress = () => 
            link.To.ShouldEqual(toAddress); 
    } 

   
}
