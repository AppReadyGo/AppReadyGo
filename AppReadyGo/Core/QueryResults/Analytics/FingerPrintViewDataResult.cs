using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Analytics.QueryResults
{
    public class FingerPrintViewDataResult : FilterDataResult
    {
        public IEnumerable<ScreenResult> Screens { get; set; }

        public Dictionary<DateTime, int> VisitsData { get; set; }

        public Dictionary<DateTime, int> ScrollsData { get; set; }

        public Dictionary<DateTime, int> ClicksData { get; set; }

        public Dictionary<String, int> ControlClicksData { get; set; }
    }
}
