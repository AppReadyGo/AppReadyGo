using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class AccountProfilerMaping : ClassMapping<AccountProfiler>
    {
        public AccountProfilerMaping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.UpdateFriquency, map => map.NotNullable(true));
            Property(x => x.Price, map => map.NotNullable(true));
            Bag(x => x.Users, map =>
            {
                map.Key(k => k.Column("ID"));
            }, r => r.OneToMany());
        }
    }
}
