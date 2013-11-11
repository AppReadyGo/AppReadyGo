using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Pages.Application;

namespace AppReadyGo.Web.Model.Pages.Analytics
{
    public class AnalyticsModel
    {
        public ApplicationModel ApplicationInfo { get; set; }

        public TaskModel TaskInfo { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        public IDictionary<int, string> Screens { get; set; }

        public string DateRange { get; set; }
    }
}