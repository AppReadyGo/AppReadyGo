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
        private Iesi.Collections.Generic.ISet<PublishDetails> publishes;
        private Iesi.Collections.Generic.ISet<Screenshot> screenshots;
        private Iesi.Collections.Generic.ISet<PageView> pageViews;

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

        public virtual string MarketUrl { get; set; }

        /// <summary>
        /// holds application screens list 
        /// </summary>
        public virtual IEnumerable<Screen> Screens { get { return screens; } }

        public virtual IEnumerable<PublishDetails> Publishes { get { return publishes; } }

        public virtual IEnumerable<Screenshot> Screenshots { get { return screenshots; } }

        public virtual IEnumerable<PageView> PageViews { get { return pageViews; } }

        public virtual string IconExt { get; protected set; }

        public virtual string PackageFileName { get; set; }

        public Application()
        {
            this.screens = new HashedSet<Screen>();
            this.publishes = new HashedSet<PublishDetails>();
            this.screenshots = new HashedSet<Screenshot>();
            this.pageViews = new HashedSet<PageView>();
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

        public virtual void Update(string name, string description, ApplicationType type)
        {
            this.Name = name;
            this.Description = description;
            this.Type = type;
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

        public virtual void Publish(PublishDetails publishDetails)
        {
            publishes.Add(publishDetails);
        }

        public virtual void UpdateIcon(string iconExt)
        {
            this.IconExt = iconExt;
        }

        public virtual void AddScreenshot(Screenshot screenshot)
        {
            this.screenshots.Add(screenshot);
        }

        public virtual void RemoveScreenshot(Screenshot screenshot)
        {
            this.screenshots.Remove(screenshot);
        }

        public virtual void AddPageView(PageView pageView)
        {
            this.pageViews.Add(pageView);
        }

        public virtual void UpdatePackage(string fileName)
        {
            this.PackageFileName = fileName;
        }
    }
}
