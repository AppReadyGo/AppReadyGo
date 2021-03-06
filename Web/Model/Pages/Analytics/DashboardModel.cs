﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Filter;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Model.Master;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using System.Web.Mvc;

namespace AppReadyGo.Model.Pages.Analytics
{
    public class DashboardModel : AnalyticsMasterModel
    {
        public FilterModel Filter { get; set; }

        public string UsageChartData { get; set; }

        public ContentOverviewModel[] ContentOverviewData { get; set; }

        public DashboardModel(FilterParametersModel filter, MenuItem selectedItem, FilterDataResult filterDataResult, bool isSingleMode)
            : base(selectedItem)
        {
            this.Filter = new FilterModel(filter, filterDataResult, isSingleMode, selectedItem);
            this.FilterUrlPart = this.Filter.GetUrlPart();
        }
    }

    public class ContentOverviewModel
    {
        public string Path { get; set; }
        public int Views { get; set; }
        public int Index { get; set; }
        public bool IndexIsOdd { get { return this.Index % 2 == 0; } }

        public int ApplicationId { get; set; }
        public int? ScreenId { get; set; }

        public string GetPathUrl(string filterPath)
        {
            int pIndx = filterPath.IndexOf("p=");
            if (pIndx > 0)
            {
                int endIndx = filterPath.IndexOf("&", pIndx);
                if (endIndx == -1)
                {
                    endIndx = filterPath.Length;
                }
                filterPath = filterPath.Replace(filterPath.Substring(pIndx, endIndx - pIndx), string.Empty);
            }
            filterPath += "&p=" + HttpUtility.UrlEncode(this.Path);
            return filterPath;
        }
    }
}