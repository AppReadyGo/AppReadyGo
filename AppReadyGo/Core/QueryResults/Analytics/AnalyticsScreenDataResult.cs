using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Analytics
{
    public class AnalyticsScreenDataResult
    {
        public int Views { get; set; }

        public int Devices { get; set; }

        public double AvgDuration { get; set; }

        public double AvgClicks { get; set; }

        public double AvgScrolls { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public class Link
        {
            public string Title { get; set; }

            public int Clicks { get; set; }
        }
    }
}
