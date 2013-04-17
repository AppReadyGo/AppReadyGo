using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Entities
{
    public class ApplicationType
    {
        public virtual int Id { get; protected set; }

        public virtual string Name { get; set; }

        public ApplicationType()
        {
        }

        public ApplicationType(string name)
        {
            this.Name = name;
        }
    }
}
