using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping.Alterations;
using WebCrawler.Domain;
using FluentNHibernate.Mapping;
using FluentNHibernate;

namespace WebCrawler.Infrastructure.NHibernateMaps.overrides
{
    public class PageMappingOverride : IAutoMappingOverride<Page>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<Page> mapping)
        {
            mapping.HasMany<Link>(x => x.Links)
                     .Access.CamelCaseField(Prefix.Underscore)
                     .Cascade.AllDeleteOrphan()
                     .Inverse();
            mapping.Map(Reveal.Member<Page>("InnerHtml")).Length(10000); //Lenght > 4000 => nvarchar(max)
        }
    }
}
