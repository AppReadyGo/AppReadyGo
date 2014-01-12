using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Application;
using AppReadyGo.Web.Model.Pages.Application;

namespace AppReadyGo.Web.Model.Pages.Analytics
{
    public class AnalyticsModel : AnalyticsMasterModel
    {
        public TaskDetailsModel TaskInfo { get; set; }

        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationType { get; set; }

        public string ApplicationContent { get; set; }

        public IDictionary<string, int> Pathes { get; set; }

        public IEnumerable<ScreenResult> ScreenList { get; set; }

        public string DateRange { get; set; }

        public Tabs Tab { get; set; }

        public string CustomTab { get; set; }

        public AnalyticsModel(Tabs tab)
            : base(MenuItem.Dashboard)
        {
            this.Tab = tab;
        }

        public AnalyticsModel(string customTab)
            : base(MenuItem.Dashboard)
        {
            this.CustomTab = customTab;
        }

        public enum Tabs
        {
            Task,
            Analytics,
            Reviews,
            Tips,
            CustomTab
        }
    }
}