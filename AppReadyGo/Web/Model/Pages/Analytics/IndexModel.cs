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

        public string ViewsGraphData { get; set; }

        public string ClicksGraphData { get; set; }

        public string ScrollsGraphData { get; set; }
    }
}