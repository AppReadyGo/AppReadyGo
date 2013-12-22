using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.QueryResults.Tasks;

namespace AppReadyGo.Core.QueryResults.Analytics
{
    public class AnalyticsScreenDataResult
    {
        public TaskDetailsResult TaskInfo { get; set; }

        public string Path { get; set; }

        public int Views { get; set; }

        public int Devices { get; set; }

        public double AvgDuration { get; set; }

        public double AvgClicks { get; set; }

        public double AvgScrolls { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        public Dictionary<int, string> ScreenList { get; set; }

        public Size ScreenSize { get; set; }

        public class Link
        {
            public string Title { get; set; }

            public int Clicks { get; set; }
        }
    }
}
