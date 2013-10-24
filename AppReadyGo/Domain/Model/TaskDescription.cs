using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Domain.Model
{
    public class TaskDescription
    {
        public virtual int Id { get; protected set; }

        public virtual string Description { get; protected set; }

        public TaskDescription()
        {
        }

        public TaskDescription(string description)
        {
            this.Description = description;
        }
    }
}
