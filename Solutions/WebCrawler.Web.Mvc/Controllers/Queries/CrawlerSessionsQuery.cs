using MvcContrib.Pagination;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCrawler.Domain;
using WebCrawler.Web.Mvc.ViewModels;

namespace WebCrawler.Web.Mvc.Controllers.Queries
{
    public class CrawlerSessionListQuery : NHibernateQuery, ICrawlerSessionListQuery
    {
        public MvcContrib.Pagination.IPagination<CrawlerSessionViewModel> GetPagedList(int page, int size)
        {
            CrawlerSession crawlerSessionAlias = null;
            CrawlerSettings settingsAlias = null;
            var query = Session.QueryOver<CrawlerSession>(() => crawlerSessionAlias).
                JoinAlias(() => crawlerSessionAlias.Settings, () => settingsAlias, JoinType.LeftOuterJoin).OrderBy(x => x.DateTime).Asc;

            var count = query.ToRowCountQuery();
            var totalCount = count.FutureValue<int>();

            var firstResult = (page - 1) * size;

            CrawlerSessionViewModel viewModel = null;

           

            var viewModels =
               query.SelectList(list => list
                                         .Select(x => x.Id).WithAlias(() => viewModel.Id)
                                         .Select(x => x.Title).WithAlias(() => viewModel.Title)
                                         .Select(x => x.DateTime).WithAlias(() => viewModel.DateTime)
                                         .Select(x => x.StartUrl).WithAlias(() => viewModel.StartUrl))
                .TransformUsing(Transformers.AliasToBean(typeof(CrawlerSessionViewModel)))
                .Skip(firstResult)
                .Take(size)
                .Future<CrawlerSessionViewModel>();

            return new CustomPagination<CrawlerSessionViewModel>(viewModels, page, size, totalCount.Value);
        }
    }
}