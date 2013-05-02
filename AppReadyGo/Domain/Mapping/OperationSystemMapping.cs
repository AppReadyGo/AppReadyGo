using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Domain.Model;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace AppReadyGo.Domain.Mapping
{
    internal class OperationSystemMapping : NameableMapping<OperationSystem>
    {
        public OperationSystemMapping()
        {
            Table("OperationSystems");
        }
    }
}
