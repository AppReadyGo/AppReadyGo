using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Domain.Model.BackOffice;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.Model
{
    public class AccountProfiler
    {
        public virtual int Id { get; set; }
        public virtual int UpdateFriquency { get; set; }
        public virtual decimal Price { get; set; }
        public virtual IList<User> Users { get; set; }
    }
}
