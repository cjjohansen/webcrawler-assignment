namespace WebCrawler.Infrastructure.NHibernateMaps
{
    using System.Linq;
    using FluentNHibernate;
    using FluentNHibernate.Automapping;
    using SharpArch.Domain.DomainModel;

    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(System.Type type)
        {

            if (type.GetInterfaces().Any(x =>
                 x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>)))
                return true;
            if (type.IsSubclassOf(typeof(ValueObject)))
                return true;

            return false;

        }

        public override bool ShouldMap(Member member)
        {
            return ( base.ShouldMap(member) && member.CanWrite);
        }

        public override bool AbstractClassIsLayerSupertype(System.Type type)
        {
            return type == typeof(EntityWithTypedId<>) || type == typeof(Entity) || type == typeof(ValueObject);
        }

        public override bool IsId(Member member)
        {
            return member.Name == "Id";
        }

        public override bool IsComponent(System.Type type)
        {


            if (type.IsSubclassOf(typeof (ValueObject)))
                return true;

            return base.IsComponent(type);
        }
    }
}
