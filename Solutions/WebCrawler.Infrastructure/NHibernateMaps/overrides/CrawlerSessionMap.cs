using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using WebCrawler.Domain;
using FluentNHibernate.Automapping;

namespace WebCrawler.Infrastructure.NHibernateMaps.overrides
{
    public class CrawlerSessionMappingOverride : IAutoMappingOverride<CrawlerSession>
    {

        public void Override(AutoMapping<CrawlerSession> mapping)
        {
            mapping.HasMany<Page>(x => x.Pages)
                   .Access.CamelCaseField(Prefix.Underscore)
                   .Cascade.AllDeleteOrphan()
                   .Inverse();
            mapping.HasMany<Link>(x => x.BrokenLinks)
                   .Access.CamelCaseField(Prefix.Underscore)
                   .Cascade.AllDeleteOrphan()
                   .Inverse();
        }
    }
}
