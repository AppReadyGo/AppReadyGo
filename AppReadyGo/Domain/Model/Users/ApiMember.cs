using System;
using System.Linq;
using System.Collections.Generic;
using AppReadyGo.Core;
using Iesi.Collections.Generic;
using AppReadyGo.Core.Entities;


namespace AppReadyGo.Domain.Model.Users
{
    public class ApiMember : User
    {
        private Iesi.Collections.Generic.ISet<DownloadedApplication> downloadedApplications = null;
        private Iesi.Collections.Generic.ISet<ApplicationType> applicationTypes = null;

        public virtual Gender? Gender { get; protected set; }
        public virtual AgeRange? AgeRange { get; protected set; }
        public virtual Country Country { get; protected set; }

        public virtual IEnumerable<DownloadedApplication> DownloadedApplications
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
            this.downloadedApplications = new HashedSet<DownloadedApplication>();
            this.applicationTypes = new HashedSet<ApplicationType>();
        }

        public ApiMember(string email, string password, string firstName, string lastName, Gender? gender, AgeRange? ageRange, Country country, string Zip, ApplicationType[] appTypes)
            : base(email, password)
        {
            this.downloadedApplications = new HashedSet<DownloadedApplication>();
            this.applicationTypes = new HashedSet<ApplicationType>();
            
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.AgeRange = ageRange;
            this.Country = country;
            this.Zip = Zip;
            if (appTypes != null)
            {
                this.applicationTypes.AddAll(appTypes);
            }
        }

        public virtual void DownloadApplication(Application application)
        {
            if (!this.downloadedApplications.Any(a => a.Application == application))
            {
                this.downloadedApplications.Add(new DownloadedApplication(application, this));
            }
        }

        public virtual void Update(string firstName, string lastName, Gender? gender, AgeRange? ageRange, Country country, string Zip, ApplicationType[] appTypes)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.AgeRange = ageRange;
            this.Country = country;
            this.Zip = Zip;
            this.applicationTypes.Clear();
            this.applicationTypes.AddAll(appTypes);
        }
    }
}
