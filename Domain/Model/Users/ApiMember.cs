using System;
using System.Collections.Generic;
using AppReadyGo.Core;
using Iesi.Collections.Generic;
using AppReadyGo.Core.Entities;


namespace AppReadyGo.Domain.Model.Users
{
    public class ApiMember : User
    {
        private Iesi.Collections.Generic.ISet<Application> downloadedApplications = null;
        private Iesi.Collections.Generic.ISet<ApplicationType> applicationTypes = null;

        public virtual Gender Gender { get; protected set; }
        public virtual short Age { get; protected set; }
        public virtual Country Country { get; protected set; }

        public virtual IEnumerable<Application> DownloadedApplications
        {
            get { return this.downloadedApplications; }
        }

        public virtual IEnumerable<ApplicationType> ApplicationTypes
        {
            get { return this.applicationTypes; }
        }

        public virtual UserType Type
        {
            get { return UserType.ApiMember; }
        }

        public ApiMember()
        {
            this.downloadedApplications = new HashedSet<Application>();
            this.applicationTypes = new HashedSet<ApplicationType>();
        }

        public ApiMember(string email, string password, string firstName, string lastName, Gender gender, short age, Country country, ApplicationType[] appTypes)
            : base(email, password)
        {
            this.downloadedApplications = new HashedSet<Application>();
            this.applicationTypes = new HashedSet<ApplicationType>();
            
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.Age = age;
            this.Country = country;
            this.applicationTypes.AddAll(appTypes);
        }

        public virtual void DownloadApplication(Application application)
        {
            if (!this.downloadedApplications.Contains(application))
            {
                this.downloadedApplications.Add(application);
            }
        }

        public virtual void Update(string firstName, string lastName, Gender gender, short age, Country country, ApplicationType[] appTypes)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.Age = age;
            this.Country = country;
            this.applicationTypes.Clear();
            this.applicationTypes.AddAll(appTypes);
        }
    }
}
