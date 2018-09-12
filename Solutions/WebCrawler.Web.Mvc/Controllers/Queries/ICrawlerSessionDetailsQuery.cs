using WebCrawler.Web.Mvc.ViewModels;
namespace WebCrawler.Web.Mvc.Controllers.Queries
{
    public interface ICrawlerSessionDetailsQuery
    {
        CrawlerSessionViewModel GetCrawlerSessionDetails(int id);
    }
}
