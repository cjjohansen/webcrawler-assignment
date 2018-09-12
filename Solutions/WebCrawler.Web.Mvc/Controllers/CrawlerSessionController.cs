using NHibernate;
using SharpArch.Domain.Commands;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebCrawler.Domain;
using WebCrawler.Tasks.Commands;
using WebCrawler.Web.Mvc.Controllers.Queries;
using WebCrawler.Web.Mvc.Controllers.ViewModels;
using WebCrawler.Web.Mvc.ViewModels;

namespace WebCrawler.Web.Mvc.Controllers
{
    public class CrawlerSessionController : Controller
    {
        //
        // GET: /CrawlerSession/
        private readonly ICrawlerSessionListQuery _crawlerSessionListQuery;
        private readonly ICommandHandler<ChangeCrawlerSessionCommand> _changeCrawlerSessionHandler;
        private ICrawlerSessionDetailsQuery _crawlerSessionDetailsQuery;

        public CrawlerSessionController(ICrawlerSessionListQuery crawlerSessionListQuery, ICommandHandler<ChangeCrawlerSessionCommand> changeCrawlerSessionHandler, ICrawlerSessionDetailsQuery crawlerSessionDetailsQuery)
        {
            _crawlerSessionListQuery = crawlerSessionListQuery;
            _crawlerSessionDetailsQuery = crawlerSessionDetailsQuery;
            _changeCrawlerSessionHandler = changeCrawlerSessionHandler;
            DefaultPageSize = 10;
        }

        public int DefaultPageSize { get; set; }


        //TODO: Add exception handling and rollback transaction 
        [Transaction]
        public ActionResult Index(int? page)
        {
            var viewModel = new CrawlerSessionListViewModel
            {
                CrawlerSessions = this._crawlerSessionListQuery.GetPagedList(page ?? 1, DefaultPageSize)
            };
            return View(viewModel.CrawlerSessions);
        }

        
        // GET: /CrawlerSession/Details/5

        public ActionResult Details(int id)
        {

            CrawlerSessionViewModel viewModel = _crawlerSessionDetailsQuery.GetCrawlerSessionDetails(id);

            return View(viewModel);
        }

        //
        // GET: /CrawlerSession/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CrawlerSession/Create

        [HttpPost]
        [Transaction]
        public ActionResult Create(CrawlerSessionViewModel crawlerSession)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var command = new ChangeCrawlerSessionCommand(
                        0,
                        crawlerSession.Title,
                        crawlerSession.StartUrl, crawlerSession.SearchDepth);

                    this._changeCrawlerSessionHandler.Handle(command);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(crawlerSession);
                }
              
            }
            catch(HibernateException hex)
            {
               if(NHibernateSession.Current.Transaction.IsActive)
                   NHibernateSession.Current.Transaction.Rollback();

               ModelState.AddModelError("", string.Format("a session with start url {0} allready exist.", crawlerSession.StartUrl));
                return View(crawlerSession);
            }
        }

        //
        // GET: /CrawlerSession/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CrawlerSession/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CrawlerSessionViewModel viewModel)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CrawlerSession/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CrawlerSession/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Transaction]
        public ActionResult StartCrawl(int id)
        {
            var crawlerSession = NHibernateSession.Current.Get<CrawlerSession>(id);
            crawlerSession.Start();


            return RedirectToAction("Details", id);
            //TODO:  Setup Eventlisteners on Clientside and Push PAge Found Events to Client
            //Use Socket framework /Signal R or other server to client http push technology
        }

       
    }
}
