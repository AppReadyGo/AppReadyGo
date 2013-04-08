using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Analytics.QueryResults
{
    public class ABCompareViewDataResult : FilterDataResult
    {
        public int SecondFilteredClicks { get; set; }

        public bool SecondHasClicks { get; set; }

        public int SecondFilteredScrolls { get; set; }

        public string SelectedSecondPath { get; set; }

        public int SecondFilteredVisits { get; set; }
    }
}
