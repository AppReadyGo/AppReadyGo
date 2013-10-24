using AppReadyGo.Core;
using AppReadyGo.Domain.Model;
using AppReadyGo.Domain.Model.Users;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Users
{
    internal class DownloadedApplicationMapping : ClassMapping<APIMemberTask>
    {
        public DownloadedApplicationMapping()
        {
            Schema("eco");
            Table("APIMemberTasks");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Rate, map => { });
            Property(x => x.Review, map => map.Length(250));

            ManyToOne(p => p.Task, map =>
            {
                map.NotNullable(true);
                map.Column("TaskID");
            });

            ManyToOne(p => p.User, map =>
            {
                map.NotNullable(true);
                map.Column("UserID");
            });
        }
    }
}
