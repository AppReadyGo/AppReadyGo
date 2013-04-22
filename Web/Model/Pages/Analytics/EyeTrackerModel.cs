using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Filter;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using System.Web.Mvc;

namespace AppReadyGo.Model.Pages.Analytics
{
    public class EyeTrackerModel : FilterModel
    {
        public int PointsOnReport { get; set; }

        public IEnumerable<ScreenResult> Screens { get; set; }

        public string UsageChartData { get; set; }

        public EyeTrackerModel(FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(filter, selectedItem, filterDataResult, isSingleMode)
        {
        }
   }
}