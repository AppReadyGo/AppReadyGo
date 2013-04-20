using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Domain.Model
{
    public class ApplicationType
    {
        public virtual int Id { get; protected set; }

        public virtual string Name { get; protected set; }

        public ApplicationType()
        { 
        }

        public ApplicationType(string name)
        {
            this.Name = name;
        }
    }
}
