using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class ApplicationMaping : ClassMapping<Application>
    {
        public ApplicationMaping()
        {
            Table("Applications");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name, map =>
            {
                map.Length(50);
                map.NotNullable(true);
            });
            Property(x => x.Description, map =>
            {
                map.Length(500);
            });
            ManyToOne(p => p.Type, map =>
            {
                map.Column("Type");
                map.NotNullable(true);
            });
            Property(x => x.IconExt, map =>
            {
                map.Length(5);
            });
            Property(x => x.CreateDate, map => map.NotNullable(true));
            ManyToOne(p => p.User, map =>
            {
                map.Column("UserID");
                map.NotNullable(true);
            });

            Set(p => p.Screens,
                map =>
                {
                    map.Key(k => k.Column("ApplicationID"));
                    map.Table("Screen");
                    map.Inverse(true);
                    map.Access(Accessor.Field);
                    map.Cascade(Cascade.All);
                },
                rel => rel.OneToMany());

            Set(
              x => x.Publishes,
              map =>
              {
                  map.Key(k => k.Column("ApplicationID"));
                  map.Access(Accessor.Field);
              },
              r => r.OneToMany());

            Set(
              x => x.Screenshots,
              map =>
              {
                  map.Key(k => k.Column("ApplicationID"));
                  map.Access(Accessor.Field);
              },
              r => r.OneToMany());
        }
    }
}
