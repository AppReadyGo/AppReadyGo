using System;
using System.Collections.Generic;
using AppReadyGo.Core;
using Iesi.Collections.Generic;
using AppReadyGo.Core.Entities;


namespace AppReadyGo.Domain.Model.Users
{
    public class Member : User
    {
        private Iesi.Collections.Generic.ISet<Application> applications = null;

        public virtual IEnumerable<Application> Applications
        {
            get { return this.applications; }
        }

        public virtual UserType Type
        {
            get { return UserType.Member; }
        }

        public Member()
        {
            this.applications = new HashedSet<Application>();
        }

        public Member(string email, string password)
            : base(email, password)
        {
            this.applications = new HashedSet<Application>();
        }

        public virtual void AddApplication(Application application)
        {
            this.applications.Add(application);
        }
    }
}
