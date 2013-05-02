using AppReadyGo.Core;
using AppReadyGo.Domain.Model;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    internal class DownloadedApplicationMapping : ClassMapping<DownloadedApplication>
    {
        public DownloadedApplicationMapping()
        {
            Table("DownloadedApplications");

            Id(x => x.Id, map => map.Generator(Generators.Identity));

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
