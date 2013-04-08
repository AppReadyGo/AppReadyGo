using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Filter;
using System.Web.Mvc;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;

namespace AppReadyGo.Model.Pages.Analytics
{
    public class UsageModel : FilterModel
    {
        public string UsageChartData { get; set; }

        public UsageModel(Controller controller, FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(controller, filter, selectedItem, filterDataResult, isSingleMode)
        {
        }
    }
}