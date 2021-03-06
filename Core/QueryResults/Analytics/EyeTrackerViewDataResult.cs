﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Analytics.QueryResults
{
    public class EyeTrackerViewDataResult : FilterDataResult
    {
        public IEnumerable<ScreenResult> Screens { get; set; }

        public Dictionary<DateTime, int> UsageData { get; set; }
    }
}
