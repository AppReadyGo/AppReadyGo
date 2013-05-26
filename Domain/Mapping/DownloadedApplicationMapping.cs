using AppReadyGo.Core;
using AppReadyGo.Domain.Model;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    internal class DownloadedApplicationMapping : ClassMapping<APIMemberApplication>
    {
        public DownloadedApplicationMapping()
        {
            Table("APIMemberApplications");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Used, map => { });
            Property(x => x.Review, map => map.Length(500));

            ManyToOne(p => p.Application, map =>
            {
                map.NotNullable(true);
                map.Column("ApplicationID");
            });

            ManyToOne(p => p.User, map =>
            {
                map.NotNullable(true);
                map.Column("UserID");
            });
        }
    }
}
