using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping
{
    internal class ApplicationTypeMapping : ClassMapping<ApplicationType>
    {
        public ApplicationTypeMapping()
        {
            Table("ApplicationTypes");

            Id(x => x.Id, map => { map.Generator(Generators.Identity); map.Column("ID"); });
            Property(x => x.Name, map => { map.NotNullable(true); map.Length(70); });
        }
    }
}

