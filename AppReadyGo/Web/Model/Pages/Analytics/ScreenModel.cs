using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Analytics
{
    public class ScreenModel : AnalyticsModel
    {
        public int Views { get; set; }

        public int Devices { get; set; }

        public int AvgDuration { get; set; }

        public int AvgClicks { get; set; }

        public int AvgScrolls { get; set; }

        public IEnumerable<object> Links { get; set; }

        public ScreenModel(string path)
            : base(path)
        {
        }

        public class LinkModel
        {
            public string Title { get; set; }

            public int Clicks { get; set; }

            public double Percents { get; set; }
        }
    }
}