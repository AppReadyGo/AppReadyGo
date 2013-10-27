using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class TaskMapping : ClassMapping<Task>
    {
        public TaskMapping()
        {
            Schema("eco");
            Table("Tasks");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            ManyToOne(p => p.Description, map => { map.Column("DescriptionID"); });
            Property(x => x.PublishDate, map => { });
            Property(x => x.AgeRange, map => { });
            Property(x => x.Gender, map => { });
            Property(x => x.Zip, map => { map.Length(10); });
            Property(x => x.CreatedDate, map => map.NotNullable(true));
            Property(x => x.Audence, map => map.NotNullable(true));
            ManyToOne(p => p.Application, map =>
            {
                map.Column("ApplicationID");
                map.NotNullable(true);
            });
            ManyToOne(p => p.Country, map =>
            {
                map.Column("CountryID");
            });

        }
    }
}