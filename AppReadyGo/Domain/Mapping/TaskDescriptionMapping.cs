using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode.Conformist;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class TaskDescriptionMapping : ClassMapping<TaskDescription>
    {
        public TaskDescriptionMapping()
        {
            Schema("eco");
            Table("TaskDescriptions");

            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(p => p.Description, map => { map.Length(250); map.NotNullable(true); });
        }
    }
}