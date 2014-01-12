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

        public double AvgDuration { get; set; }

        public double AvgClicks { get; set; }

        public double AvgScrolls { get; set; }

        public IEnumerable<LinkModel> Links { get; set; }

        public string UrlPart { get; set; }

        public int? ScreenId { get; set; }

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