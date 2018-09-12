using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Infrastructure.NHibernateMaps.Conventions
{
    public class DomainSignatureConvention : AttributePropertyConvention<DomainSignatureAttribute>
    {
        protected override void Apply(DomainSignatureAttribute attribute, IPropertyInstance instance)
        {
            instance.UniqueKey(instance.EntityType.Name + "DomainSignature");
        }
    }
}
