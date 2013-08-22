﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class ScreenMapping : ClassMapping<Screen>
    {
        public ScreenMapping()
        {
            Table("Screens");
            
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            //Property(p => p.ApplicationId, map => { map.NotNullable(true); });
            ManyToOne(p => p.Application, map =>
            {
                map.NotNullable(true);
                map.Column("ApplicationId");
               
            });
            
            Property(p => p.Path, map => { map.NotNullable(true); map.Length(256); });
            Property(p => p.Height, map => map.NotNullable(true));
            Property(p => p.Width, map => map.NotNullable(true));
            Property(p => p.FileExtension, map => { map.NotNullable(true); map.Length(5); });
        }
    }
}