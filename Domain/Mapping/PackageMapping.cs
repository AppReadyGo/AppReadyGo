using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    public class PackageMapping : ClassMapping<Package>
    {
        public PackageMapping()
        {
            Table("Packages");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.FileName, map => { map.NotNullable(true); map.Length(50); });
            Property(x => x.CreatedDate, map => map.NotNullable(true));
        }
    }
}