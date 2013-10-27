using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AppReadyGo.Model.Filter;
using System.Web.Mvc;

namespace AppReadyGo.Model.Master
{
    public class AnalyticsMasterModel : AfterLoginMasterModel
    {
        public MenuItem SelectedMenuItem { get; private set; }

        public string FilterUrlPart { get; protected set; }

        /// <summary>
        /// Disable menu when Filter does not have url
        /// </summary>
        public bool IsMenuDisabled { get { return string.IsNullOrEmpty(this.FilterUrlPart); } }

        public AnalyticsMasterModel(MenuItem selectedItem)
            : base(AfterLoginMasterModel.MenuItem.Analytics)
        {
            this.SelectedMenuItem = selectedItem;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedMenuItem ? "active" : string.Empty;
        }

        public new enum MenuItem
        {
            Portfolios,
            Dashboard,
            TouchMap,
            EyeTracker,
            ABCompare,
            Usage,
            Visitors,
            MapOverlay,
            TraficSources,
            ContentOverview
        }
    }
}