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
        private Iesi.Collections.Generic.ISet<APIMemberApplication> applications = null;
        private Iesi.Collections.Generic.ISet<ApplicationType> applicationTypes = null;

        public virtual Gender? Gender { get; protected set; }
        public virtual AgeRange? AgeRange { get; protected set; }
        public virtual Country Country { get; protected set; }

        public virtual IEnumerable<APIMemberApplication> Applications
        {
            get { return this.applications; }
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
            this.applications = new HashedSet<APIMemberApplication>();
            this.applicationTypes = new HashedSet<ApplicationType>();
        }

        public ApiMember(string email, string password, string firstName, string lastName, Gender? gender, AgeRange? ageRange, Country country, string Zip, ApplicationType[] appTypes)
            : base(email, password)
        {
            this.applications = new HashedSet<APIMemberApplication>();
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

        public ApiMember(string email, string firstName, string lastName, Gender? gender, AgeRange? ageRange, Country country, string Zip, ApplicationType[] appTypes)
            : base(email)
        {
            this.applications = new HashedSet<APIMemberApplication>();
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
            if (!this.applications.Any(a => a.Application == application))
            {
                this.applications.Add(new APIMemberApplication(application, this));
            }
        }

        public virtual void Update(string email, string password, string firstName, string lastName, Gender? gender, AgeRange? ageRange, Country country, string zip, ApplicationType[] appTypes)
        {
            this.Password = Encryption.SaltedHash(password, this.PasswordSalt);
            this.Update(email, firstName, lastName, gender, ageRange, country, zip, appTypes);
        }

        public virtual void Update(string email, string firstName, string lastName, Gender? gender, AgeRange? ageRange, Country country, string zip, ApplicationType[] appTypes)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.AgeRange = ageRange;
            this.Country = country;
            this.Zip = zip;
            this.applicationTypes.Clear();
            if (appTypes != null)
            {
                this.applicationTypes.AddAll(appTypes);
            }
        }
        public virtual void UserApplicationWasUsed(Application application)
        {
            var app = this.applications.FirstOrDefault(a => a.Application == application);
            if (app != null)
            {
                app.ApplicationWasUsed();
            }
        }

        public virtual void UpdateApplicationReview(Application application, string review)
        {
            var app = this.applications.FirstOrDefault(a => a.Application == application);
            if (app != null)
            {
                app.UpdateReview(review);
            }
        }
    }
}
