using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Analytics
{
    public class IndexModel : AnalyticsModel
    {
        public int Downloads { get; set; }

        public int Devices { get; set; }

        public int Screens { get; set; }

        public int Sessions { get; set; }

        public int AvSessionDuration { get; set; }

        public int AvClicks { get; set; }

        public int AvScrolls { get; set; }

        public KeyValuePair<string, int>[] ViewsGraphData { get; set; }

        public KeyValuePair<string, int>[] ClicksGraphData { get; set; }

        public KeyValuePair<string, int>[] ScrollsGraphData { get; set; }

        public IndexModel()
            : base(Tabs.Task)
        {
        }
    }
}