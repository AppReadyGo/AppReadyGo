using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class ApiMemberTaskMapping : ClassMapping<ApiMemberTask>
    {
        public ApiMemberTaskMapping()
        {
            Schema("eco");
            Table("ApiMemberTasks");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Review, map => { map.Length(250); });
            Property(x => x.Rate, map => {  });
            ManyToOne(p => p.User, map =>
            {
                map.Column("UserID");
                map.NotNullable(true);
            });
            ManyToOne(p => p.Task, map =>
            {
                map.Column("TaskID");
            });

        }
    }
}