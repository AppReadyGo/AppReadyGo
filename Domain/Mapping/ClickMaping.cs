﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class ClickMaping : ClassMapping<Click>
    {
        public ClickMaping()
        {
            Table("Clicks");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Date, map => map.NotNullable(true));
            Property(p => p.X, map => map.NotNullable(true));
            Property(p => p.Y, map => map.NotNullable(true));
            Property(p => p.Orientation, map => map.NotNullable(true));
            ManyToOne(p => p.PageView, map =>
                {
                    map.NotNullable(true);
                    map.Column("PageViewId");
                    map.Cascade(Cascade.All);
                });
        }
    }
}
