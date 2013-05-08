using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using AppReadyGo.Model.Filter;
using AppReadyGo.Model.Master;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AppReadyGo.Model.Pages.Analytics
{
    public class ABCompareModel : AnalyticsMasterModel
    {
        public FilterModel Filter { get; set; }

        public IEnumerable<SelectListItem> FirstScreenPathes { get; set; }

        public IEnumerable<SelectListItem> SecondScreenPathes { get; set; }

        public string FirstPath { get; set; }

        public string SecondPath { get; set; }

        public string FirstScreenUrlPart
        {
            get
            {
                return this.Filter.GetUrlPart(this.FirstPath);
            }
        }

        public string SecondScreenUrlPart
        {
            get
            {
                return this.Filter.GetUrlPart(this.SecondPath);
            }
        }

        public bool FirstHasFilteredClicks { get; set; }
        public bool FirstHasClicks { get; set; }

        public bool SecondHasFilteredClicks { get; set; }
        public bool SecondHasClicks { get; set; }

        public ABCompareModel(FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(selectedItem)
        {
            this.Filter = new FilterModel(filter, filterDataResult, isSingleMode, selectedItem);
            this.FilterUrlPart = this.Filter.GetUrlPart();
        }
    }
}