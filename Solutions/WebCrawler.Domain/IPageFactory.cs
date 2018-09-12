using System;

namespace WebCrawler.Domain
{
    public interface IPageFactory
    {
        Page CreatePage(Uri address, int pageDepth, CrawlerSession crawlerSession);
    }
}