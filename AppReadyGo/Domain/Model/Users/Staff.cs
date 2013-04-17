using System;
using System.Collections.Generic;
using AppReadyGo.Core;
using Iesi.Collections.Generic;
using AppReadyGo.Core.Entities;


namespace AppReadyGo.Domain.Model.Users
{
    public class Staff : User
    {
        private Iesi.Collections.Generic.ISet<StaffRole> roles = null;

        public virtual IEnumerable<StaffRole> Roles
        {
            get { return this.roles; }
        }

        public override UserType Type
        {
            get { return UserType.Staff; }
        }

        public override Membership Membership
        {
            get { return Membership.Pro; }
        }

        public Staff(string email, string password)
            : base(email, password)
        {
            this.roles = new HashedSet<StaffRole>();
        }

        protected Staff()
            : base()
        {
            this.roles = new HashedSet<StaffRole>();
        }


        public virtual void GrantRole(StaffRole role)
        {
            if (!this.roles.Contains(role))
            {
                this.roles.Add(role);
            }
        }

        public virtual void RevokeRole(StaffRole role)
        {
            if (!this.roles.Contains(role))
            {
                this.roles.Remove(role);
            }
        }
    }
}
