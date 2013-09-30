using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class PublishDetailsMapping : ClassMapping<PublishDetails>
    {
        public PublishDetailsMapping()
        {
            Table("Publishes");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.AgeRange, map => { });
            Property(x => x.Gender, map => { });
            Property(x => x.Zip, map => { map.Length(10); });
            Property(x => x.CreatedDate, map => map.NotNullable(true));
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