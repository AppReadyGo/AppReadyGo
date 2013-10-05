using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Filter;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using System.Web.Mvc;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Model.Pages.Analytics
{
    public class FingerPrintModel : AnalyticsMasterModel
    {
        public FilterModel Filter { get; set; }

        public int PointsOnReport { get; set; }

        public IEnumerable<ScreenResult> Screens { get; set; }

        public string GraphsData { get; set; }

        public int VisitsAmount { get; set; }

        public  Dictionary<String, int> ControlClicks  { get; set; }

        public FingerPrintModel(FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(selectedItem)
        {
            this.Filter = new FilterModel(filter, filterDataResult, isSingleMode, selectedItem);
            this.FilterUrlPart = this.Filter.GetUrlPart();
        }
    }
}