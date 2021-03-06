﻿using AppReadyGo.Domain.Model.Content;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AppReadyGo.Domain.Mapping.Content
{
    internal class SystemMailMapping : ClassMapping<SystemMail>
    {
        public SystemMailMapping()
        {
            Schema("cont");
            Table("Mails");

            Discriminator(map => map.Column("IsSystem"));
            DiscriminatorValue(true);
            Property(p => p.IsSystem, p => { p.NotNullable(true); p.Column("IsSystem"); p.Access(Accessor.ReadOnly); p.Insert(false); p.Update(false); });

            Id(x => x.Id, map => { map.Column("ID"); map.Generator(Generators.Identity); });
            Property(x => x.Url, map => { map.NotNullable(true); map.Length(256); });
            ManyToOne(x => x.Theme, map => { map.NotNullable(true); map.Column("ThemeID"); });
            Property(x => x.IsSystem, map => { map.NotNullable(true); });
            Set(
                x => x.Items,
                map =>
                {
                    map.Access(Accessor.Field);
                    map.Cascade(Cascade.All);
                    map.Inverse(true);
                    map.Key(x => x.Column("MailID"));
                },
                r => r.OneToMany());
        }
    }

    internal class MailMapping : SubclassMapping<Mail>
    {
        public MailMapping()
        {
            DiscriminatorValue(false);
        }
    }

}
