using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class ScreenshotMapping : ClassMapping<Screenshot>
    {
        public ScreenshotMapping()
        {
            Table("Screenshots");
            
            Id(p => p.Id, map => map.Generator(Generators.Identity));
            //Property(p => p.ApplicationId, map => { map.NotNullable(true); });
            ManyToOne(p => p.Application, map =>
            {
                map.NotNullable(true);
                map.Column("ApplicationId");
               
            });

            Property(p => p.FileExtension, map => { map.NotNullable(true); map.Length(10); });
        }
    }
}
