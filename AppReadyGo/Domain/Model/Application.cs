using System;
using System.Collections.Generic;
using AppReadyGo.Core.Entities;
using NHibernate.Collection.Generic;
using AppReadyGo.Domain.Model.Users;
using Iesi.Collections.Generic;

namespace AppReadyGo.Domain.Model
{

    /// <summary>
    /// this class represents application data mode 
    /// instance of this class holds information about one single applications 
    /// and describes application properties 
    /// </summary>
    public class Application
    {
        private Iesi.Collections.Generic.ISet<Screen> screens;

        /// <summary>
        /// application ID 
        /// </summary>
        public virtual int Id { get; protected set; }
        /// <summary>
        /// Application name
        /// </summary>
        public virtual string Name { get; protected set; }
        /// <summary>
        /// Application description
        /// </summary>
        public virtual string Description { get; protected set; }
        /// <summary>
        /// Application creation date - when the application created in our system 
        /// </summary>
        public virtual DateTime CreateDate { get; protected set; }
        /// <summary>
        /// Member to represent OS platform type 
        /// </summary>
        public virtual ApplicationType Type { get; protected set; }
        /// <summary>
        /// this member holds User 
        /// </summary>
        public virtual User User { get; protected set; }
        /// <summary>
        /// holds application screens list 
        /// </summary>
        public virtual IEnumerable<Screen> Screens { get { return screens; } }

        public virtual PublishDetails Package { get; protected set; }

        public virtual string IconExt { get; protected set; }

        public Application()
        {
            this.screens = new HashedSet<Screen>();
            this.CreateDate = DateTime.UtcNow;
        }

        public Application(Member user, string name, string description, ApplicationType type, string iconExt)
            : this()
        {
            user.AddApplication(this);
            this.Name = name;
            this.Description = description;
            this.Type = type;
            this.User = user;
            this.IconExt = iconExt;
        }

        protected internal virtual void Update(string description)
        {
            this.Description = description;
        }

        public virtual void AddScreen(Screen screen)
        {
            this.screens.Add(screen);
        }

        public virtual void RemoveScreen(Screen screen)
        {
            if (this.screens.Contains(screen))
            {
                this.screens.Remove(screen);
            }
        }

        public virtual void SetPakage(PublishDetails package)
        {
            this.Package = package;
        }
    }
}
