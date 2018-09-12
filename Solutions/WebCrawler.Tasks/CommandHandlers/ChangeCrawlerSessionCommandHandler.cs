using MvcContrib.PortableAreas;
using SharpArch.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpArch.NHibernate.Contracts.Repositories;
using WebCrawler.Tasks.Commands;
using WebCrawler.Domain;

namespace WebCrawler.Tasks.CommandHandlers
{
  
    public class ChangeCrawlerSessionHandler : ICommandHandler<ChangeCrawlerSessionCommand>
    {
        private readonly INHibernateRepository<CrawlerSession> _crawlerSessionRepository;

        public ChangeCrawlerSessionHandler(INHibernateRepository<CrawlerSession> crawlerSessionRepository)
        {
            this._crawlerSessionRepository = crawlerSessionRepository;
        }

        public void Handle(ChangeCrawlerSessionCommand command)
        {
            CrawlerSession crawlerSession;
            if(command.Id != 0)
                crawlerSession = this._crawlerSessionRepository.Get(command.Id) ?? new CrawlerSession();
            else
                crawlerSession = new CrawlerSession();
          
            crawlerSession.ChangeSession(
                command.Title,
                command.StartUrl,
                command.SearchDepth);

            this._crawlerSessionRepository.SaveOrUpdate(crawlerSession);
            
        }
    }
}
