using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public IEnumerable<string> Pathes { get; set; }

        public IDictionary<int, string> ScreenList { get; set; }

        public string DateRange { get; set; }

        public AnalyticsModel()
            : base(MenuItem.Dashboard)
        {
        }
    }
}