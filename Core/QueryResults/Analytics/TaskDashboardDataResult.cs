using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Core.QueryResults.Tasks;

namespace AppReadyGo.Core.QueryResults.Analytics
{
    public class TaskDashboardDataResult
    {
        public TaskDetailsResult TaskInfo { get; set; }

        public int Downloads { get; set; }

        public int Devices { get; set; }

        public int Screens { get; set; }

        public int Sessions { get; set; }

        public int AvSessionDuration { get; set; }

        public int AvClicks { get; set; }

        public int AvScrolls { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        //public IDictionary<int, string> Screens { get; set; }

        // TODO: Date range from publish date till today
        //public string DateRange { get; set; }

        public string ViewsGraphData { get; set; }

        public string ClicksGraphData { get; set; }

        public string ScrollsGraphData { get; set; }
    }
}
