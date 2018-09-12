using System;
using System.IO;
using System.Net;

namespace WebCrawler.Domain
{
    public interface IPageRequester
    {
        string SendWebRequestForPage(Uri url);
    }

    public class PageRequester : IPageRequester
    {
        public PageRequester()
        {
        }

        public string SendWebRequestForPage(Uri url)
        {

            HttpWebRequest request = null;

          
            request = (HttpWebRequest)WebRequest.Create(url);
          

            request.UserAgent = "Web Crawler";

            var response = request.GetResponse();

            var stream = response.GetResponseStream();

            var reader = new StreamReader(stream);

            string htmlText = reader.ReadToEnd();

            return htmlText;

        }
    }
}