using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Domain.Model
{
    public class Nameable
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
