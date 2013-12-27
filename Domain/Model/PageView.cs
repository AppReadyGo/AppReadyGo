using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Domain.Model.Events;
using Iesi.Collections.Generic;

namespace AppReadyGo.Domain.Model
{
    public class PageView
    {

        private Iesi.Collections.Generic.ISet<Click> clicks;
        private Iesi.Collections.Generic.ISet<ViewPart> viewParts;
        private Iesi.Collections.Generic.ISet<Scroll> scrolls;
        private Iesi.Collections.Generic.ISet<ControlClick> controlClicks;

        public virtual long Id { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime Date { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Path { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual PageView PreviousPageView { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Language Language { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Country Country { get; protected set; }

        public virtual string City { get; protected set; }

        public virtual OperationSystem OperationSystem { get; protected set; }

        public virtual Browser Browser { get; protected set; }

        public virtual int ScreenWidth { get; protected set; }

        public virtual int ScreenHeight { get; protected set; }

        public virtual int ClientWidth { get; protected set; }

        public virtual int ClientHeight { get; protected set; }

        public virtual int TaskId { get; protected set; }
        public virtual int UserId { get; protected set; }

        public virtual Application Application { get; protected set; }

        public virtual IEnumerable<Click> Clicks { get { return clicks; } }

        public virtual IEnumerable<ViewPart> ViewParts { get { return viewParts; } }

        public virtual IEnumerable<Scroll> Scrolls { get { return scrolls; } }

        public virtual IEnumerable<ControlClick> ControlClicks { get { return controlClicks; } }

        public PageView()
        {
            this.clicks = new HashedSet<Click>();
            this.viewParts = new HashedSet<ViewPart>();
            this.scrolls = new HashedSet<Scroll>();
            this.controlClicks = new HashedSet<ControlClick>();
        }

        public PageView(
            Application application, 
            DateTime date, 
            string path, 
            PageView previousPageView, 
            Language language, 
            Country country, 
            string city,
            OperationSystem operationSystem, 
            Browser browser, 
            int screenWidth, 
            int screenHeight,
            int clientWidth,
            int clientHeight,
            int taskId,
            int userId)
            : this()
        {
            this.Date = date;
            this.Path = path;
            this.PreviousPageView = previousPageView;
            this.Language = language;
            this.Country = country;
            this.City = city;
            this.OperationSystem = operationSystem;
            this.Browser = browser;
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;
            this.Application = application;
            this.TaskId = taskId;
            this.UserId = userId;
        }

        public virtual void AddClick(Click click)
        {
            this.clicks.Add(click);
        }

        public virtual void AddViewPart(ViewPart viewPart)
        {
            this.viewParts.Add(viewPart);
        }

        public virtual void AddScroll(Scroll scroll)
        {
            this.scrolls.Add(scroll);
        }

        public virtual void AddControlClick(ControlClick controlClick)
        {
            this.controlClicks.Add(controlClick);
        }
    }
}
