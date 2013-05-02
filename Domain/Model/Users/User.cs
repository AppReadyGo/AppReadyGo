using System;
using System.Collections.Generic;
using AppReadyGo.Core;
using Iesi.Collections.Generic;
using AppReadyGo.Core.Entities;


namespace AppReadyGo.Domain.Model.Users
{
    public abstract class User
    {
        public virtual int Id { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual string PasswordSalt { get; protected set; }
        public virtual DateTime CreateDate { get; protected set; }
        public virtual DateTime? LastAccessDate { get; protected set; }
        public virtual bool Activated { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual bool Unsubscribed { get; protected set; }
        public virtual bool SpecialAccess { get; protected set; }
        public virtual bool AcceptedTermsAndConditions { get; protected set; }
        public virtual Membership Membership { get; protected set; }
        public virtual string Zip { get; protected set; }

        public virtual UserType Type
        {
            get { return UserType.Member; }
        }

        protected User()
        {
            this.CreateDate = DateTime.UtcNow;
            this.Membership = Membership.Basic;
            this.PasswordSalt = Encryption.GenerateSalt();
        }

        public User(string email, string password)
            : this()
        {
            this.Email = email;
            this.Password = Encryption.SaltedHash(password, this.PasswordSalt);
        }

        public virtual void ChangePassword(string password)
        {
            this.Password = Encryption.SaltedHash(password, this.PasswordSalt);
        }

        public virtual void Activate()
        {
            this.Activated = true;
        }

        public virtual void Deactivate()
        {
            this.Activated = false;
        }

        public virtual void Unsubscribe()
        {
            this.Unsubscribed = true;
        }

        public virtual void UpdateLastAccess()
        {
            this.LastAccessDate = DateTime.UtcNow;
        }

        public virtual void AcceptTermsAndConditions(bool reset = false)
        {
            this.AcceptedTermsAndConditions = !reset;
        }

        public virtual void GrantSpecialAccess(bool specialAccess)
        {
            this.SpecialAccess = specialAccess;
        }
    }
}