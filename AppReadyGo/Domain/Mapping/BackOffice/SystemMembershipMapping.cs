﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model.BackOffice;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping.BackOffice
{/*
    public class SystemMembershipMapping : ClassMapping<SystemMembership>
    {
        public SystemMembershipMapping()
        {
            Table("aspnet_Membership");
            //ManyToOne(p => p.User, map =>
            //{
            //    map.Lazy(LazyRelation.NoLazy);
            //    map.Column("UserId");
            //    map.NotNullable(true);
            //});
            Property(x => x.Email, map =>
            {
                map.Length(255);
                map.NotNullable(true);
            });
            SchemaAction(NHibernate.Mapping.ByCode.SchemaAction.None);
        }
    }
  * */
}
