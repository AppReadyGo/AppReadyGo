using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    public class PublishDetailsMapping : ClassMapping<PublishDetails>
    {
        public PublishDetailsMapping()
        {
            Table("Publishes");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.AgeRange, map => { });
            Property(x => x.CreatedDate, map => map.NotNullable(true));
        }
    }
}