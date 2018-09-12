using Machine.Specifications;
using System;
using WebCrawler.Domain;


namespace MSpecTests.WebCrawler
{
   
    [Subject(typeof(Link), "Link construction")] 
    public class when_creating_a_link
    { 
        static Uri fromAddress;
        static Uri toAddress;
      
        static Link link;


        private Establish context = () =>
            {
                fromAddress = new Uri(@"http://localhost.webcravler.com");
                toAddress = new Uri(@"http://localhost.webcravler.com/linkA");
            };

        private Because of = () =>
                         link =  new Link(fromUrl : fromAddress, toUrl : toAddress);
         
        It should_have_its_From_property_returning_fromAddress = () => 
            link.FromUrl.ShouldEqual(fromAddress); 
         
        It should_should_have_its_To_property_returning_ToAddress = () => 
            link.ToUrl.ShouldEqual(toAddress); 
    } 

   
}
