using System;
using NHibernate.Transform;
using SharpArch.NHibernate;
using WebCrawler.Domain;
using WebCrawler.Web.Mvc.ViewModels;
using System.Collections.Generic;
using System.Collections;
using NHibernate.Criterion;
using System.Linq;
namespace WebCrawler.Web.Mvc.Controllers.Queries
{
    public class CrawlerSessionDetailsQuery : NHibernateQuery,ICrawlerSessionDetailsQuery
    {

        public CrawlerSessionViewModel GetCrawlerSessionDetails(int id)
        {
            CrawlerSession crawlerSessionAlias = null;
            CrawlerSessionViewModel viewModelAlias;
            var viewModel = Session.QueryOver<CrawlerSession>(() => crawlerSessionAlias).Where(x => x.Id == id)

                                   .Select(Projections.ProjectionList()
                                                      .Add(Projections.Property<CrawlerSession>(cs => cs.Title))
                                                      .Add(Projections.Property<CrawlerSession>(cs => cs.StartUrl))
                                                      .Add(Projections.Property<CrawlerSession>(cs => cs.DateTime)))
                                   .List<object[]>()


                                   .Select(values => new CrawlerSessionViewModel()
                                       {
                                           Title = (string) values[0],
                                           StartUrl = (string) values[1],
                                           DateTime =  values[2] != null ? ((DateTime) values[2]).ToShortTimeString() : "-"

                                       });

            return  viewModel.FirstOrDefault() ;
        }
    }
}