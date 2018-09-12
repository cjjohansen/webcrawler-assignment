using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Domain.Attributes;

namespace WebCrawler.Infrastructure.NHibernateMaps.Conventions
{
    public class IndexableConvention : AttributePropertyConvention<IndexableAttribute>
    {
        protected override void Apply(IndexableAttribute attribute, IPropertyInstance instance)
        {
            instance.Index(attribute.GetName());
        }
    }
}
