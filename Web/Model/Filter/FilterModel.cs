﻿using AppReadyGo.Core;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AppReadyGo.Web.Model.Shared;

namespace AppReadyGo.Model.Filter
{
    public class FilterModel
    {
        public int SelectedApplicationId { get; protected set; }
        public DateTime SelectedDateFrom { get; protected set; }
        public DateTime SelectedDateTo { get; protected set; }
        public string SelectedScreenSize { get; protected set; }
        public string SelectedPath { get; protected set; }

        public IEnumerable<SelectListItem> SelectListPathes { get; protected set; }
        public IEnumerable<SelectListItem> SelectListScreenSizes { get; protected set; }

        public string FormAction { get; protected set; }

        public string PlaceHolderHTML { get; protected set; }

        public bool NoData { get; protected set; }
        public int ClicksAmount { get; protected set; }
        public bool HasScrolls { get; protected set; }
        public int ScrollsAmount { get; protected set; }
        public bool HasClicks { get; protected set; }
        public bool HasControlClicks { get; protected set; }
        public int CobntrolClicksAmount { get; protected set; }

        //Top Panel
        public string Title { get; protected set; }
        public bool IsSingleMode { get; protected set; }
        public string ApplicationName { get; protected set; }

        public int? ScreenId { get; set; }
        public IEnumerable<string> Pathes { get; set; }

        public TesterWidgetModel TesterWidget { get; set; }
    
        public FilterModel(FilterParametersModel filter, FilterDataResult filterDataResult, bool isSingleMode, AnalyticsMasterModel.MenuItem leftMenuSelectedItem, string placeHolderHTML = null)
        {
            this.PlaceHolderHTML = placeHolderHTML;

            this.IsSingleMode = isSingleMode;

            this.SelectedDateFrom = filter.FromDate;
            this.SelectedDateTo = filter.ToDate;

            var js = new JavaScriptSerializer();

            this.FormAction = leftMenuSelectedItem.ToString();

            var sizes = new List<SelectListItem>();
            var pathes = new List<SelectListItem>();


            if (filterDataResult.ScreenData != null)
            {
                this.ClicksAmount = filterDataResult.ScreenData.ClicksAmount;
                this.HasScrolls = filterDataResult.ScreenData.HasScrolls;
                this.ScreenId = filterDataResult.ScreenData.Id;
                this.HasClicks = filterDataResult.ScreenData.HasClicks;
                this.ScrollsAmount = filterDataResult.ScreenData.ScrollsAmount;
                this.HasControlClicks = filterDataResult.ScreenData.HasControlClicks;
                this.CobntrolClicksAmount = filterDataResult.ScreenData.ControlClicksAmount;                         
            }

            this.NoData = !filterDataResult.Applications.SelectMany(a => a.ScreenSizes).Any();

            var curApplication = filterDataResult.Applications.Single(a => a.Id == filter.TaskId);

            this.Pathes = curApplication.Pathes;

            this.SelectedApplicationId = curApplication.Id;
            this.ApplicationName = curApplication.Name;
                    
            this.SelectedScreenSize = filter.ScreenSize.HasValue ? filter.ScreenSize.Value.ToFormatedString() : (isSingleMode ? curApplication.ScreenSizes.First().ToFormatedString() : null);
            this.SelectedPath = string.IsNullOrEmpty(filter.Path) ? (isSingleMode ? curApplication.Pathes.First() : null) : filter.Path;

            sizes.AddRange(curApplication.ScreenSizes.Select(s => new SelectListItem { Value = s.ToFormatedString(), Text = s.ToFormatedString(), Selected = s.ToFormatedString() == this.SelectedScreenSize }));
            pathes.AddRange(this.Pathes.Select(p => new SelectListItem { Value = p, Text = p, Selected = p == this.SelectedPath }));

            if (!isSingleMode)
            {
                sizes.Insert(0, new SelectListItem { Value = "", Text = "All Sizes", Selected = this.SelectedScreenSize == null });
                pathes.Insert(0, new SelectListItem { Value = "", Text = "All Pathes", Selected = this.SelectedPath == null });
            }

            this.SelectListScreenSizes = sizes;
            this.SelectListPathes = pathes;

            this.TesterWidget = new TesterWidgetModel{
                Count = filterDataResult.ApiMemberApplications.Count(),
                LastTesters = filterDataResult.ApiMemberApplications.Select(a => "/content/images/no_image_60x60.png")
            };
       }

        public string GetUrlPart(string path = null)
        {
            if (this.NoData)
            {
                return null;
            }
            else
            {
                path = string.IsNullOrEmpty(path) ? this.SelectedPath : path;

                var parts = new List<string>() { string.Format("aid={0}", this.SelectedApplicationId) };
                if (!string.IsNullOrEmpty(this.SelectedScreenSize)) parts.Add(string.Format("ss={0}", this.SelectedScreenSize));

                if (!string.IsNullOrEmpty(path)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(path)));

                parts.Add(string.Format("fd={0}", this.SelectedDateFrom.ToString("dd-MMM-yyyy")));
                parts.Add(string.Format("td={0}", this.SelectedDateTo.ToString("dd-MMM-yyyy")));
                return "?" + string.Join("&", parts.ToArray());
            }
        }

        public static string GetUrlPart(int taskId, int applicationId, string screenSize, string path, DateTime dateFrom, DateTime dateTo)
        {
            var parts = new List<string>() { string.Format("tid={0}&aid={1}", taskId, applicationId) };
            if (!string.IsNullOrEmpty(screenSize)) parts.Add(string.Format("ss={0}", screenSize));

            if (!string.IsNullOrEmpty(path)) parts.Add(string.Format("p={0}", HttpUtility.UrlEncode(path)));

            parts.Add(string.Format("fd={0}", dateFrom.ToString("dd-MMM-yyyy")));
            parts.Add(string.Format("td={0}", dateTo.ToString("dd-MMM-yyyy")));
            return "?" + string.Join("&", parts.ToArray());
        }
    }
}