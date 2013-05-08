using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Filter;
using System.Web.Mvc;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Model.Pages.Analytics
{
    public class UsageModel : AnalyticsMasterModel
    {
        public FilterModel Filter { get; set; }

        public string UsageChartData { get; set; }

        public UsageModel(FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(selectedItem)
        {
            this.Filter = new FilterModel(filter, filterDataResult, isSingleMode, selectedItem);
            this.FilterUrlPart = this.Filter.GetUrlPart();
        }
    }
}