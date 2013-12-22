using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AppReadyGo.Common;
using AppReadyGo.Core;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Model.Filter;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Analytics;
using AppReadyGo.Model.Pages.Application;
using AppReadyGo.Models;
using AppReadyGo.Web.Model.Pages.Analytics;
using AppReadyGo.Web.Model.Pages.Application;

namespace AppReadyGo.Controllers
{
    [Authorize]
    public class AnalyticsController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(int id/*, FilterParametersModel filter*/)
        {
            var data = ObjectContainer.Instance.RunQuery(new TaskDashboardDataQuery(id));
            IndexModel model = new IndexModel
            {
                TaskInfo = new TaskDetailsModel
                {
                    Id = id,
                    Description = data.TaskInfo.Description,
                    Target = data.TaskInfo.GetTarget(),
                    Status = data.TaskInfo.GetStatus(),
                    Audence = data.TaskInfo.Audence,
                    Published = data.TaskInfo.PublishDate.HasValue ? data.TaskInfo.PublishDate.Value.ToString() : string.Empty,
                },
                ApplicationName = data.TaskInfo.ApplicationName,
                ApplicationType = data.ApplicationType,
                DateRange = data.TaskInfo.PublishDate.Value.ToString("dd MMM yyyy") + " - " + DateTime.UtcNow.ToString("dd MMM yyyy"),
                Pathes = data.Pathes.OrderBy(p => p),
                ScreenList = data.ScreenList,
                ClicksGraphData = data.ClicksGraphData.Count > 4 ? data.ClicksGraphData.OrderByDescending(x => x.Value).Take(4).ToArray() : data.ClicksGraphData.ToArray(),
                ViewsGraphData = data.ViewsGraphData.Count > 4 ? data.ViewsGraphData.OrderByDescending(x => x.Value).Take(4).ToArray() : data.ViewsGraphData.ToArray(),
                ScrollsGraphData = data.ScrollsGraphData.Count > 4 ? data.ScrollsGraphData.OrderByDescending(x => x.Value).Take(4).ToArray() : data.ScrollsGraphData.ToArray(),
            };
            if (ModelState.IsValid)
            {
            }
            return View(model);
        }

        public ActionResult Screen(int id, int taskId)
        {
            var data = ObjectContainer.Instance.RunQuery(new AnalyticsScreenDataQuery(taskId, id));
            var model = new AppReadyGo.Web.Model.Pages.Analytics.ScreenModel(data.Path)
            {
                TaskInfo = new TaskDetailsModel
                {
                    Id = taskId,
                    Description = data.TaskInfo.Description,
                    Target = data.TaskInfo.GetTarget(),
                    Status = data.TaskInfo.GetStatus(),
                    Audence = data.TaskInfo.Audence,
                    Published = data.TaskInfo.PublishDate.HasValue ? data.TaskInfo.PublishDate.Value.ToString() : string.Empty,
                },
                DateRange = data.TaskInfo.PublishDate.Value.ToString("dd MMM yyyy") + " - " + DateTime.UtcNow.ToString("dd MMM yyyy"),
                Pathes = data.Pathes.OrderBy(p => p),
                ScreenList = data.ScreenList,
                Links = new AppReadyGo.Web.Model.Pages.Analytics.ScreenModel.LinkModel[0],
                UrlPart = FilterModel.GetUrlPart(taskId, data.TaskInfo.ApplicationId, data.ScreenSize.ToFormatedString(), data.Path, data.TaskInfo.PublishDate.Value, DateTime.UtcNow) 
            };
            return View(model);
        //            public int TaskId { get; set; }

        //public DateTime FromDate { get; set;  }

        //public DateTime ToDate { get; set; }

        //public Size? ScreenSize { get; set; }

        //public string Path { get; set; }

        }

        public ActionResult Dashboard(int id, FilterParametersModel filter)
        {
            log.WriteInformation("Dashboard");
            if (ModelState.IsValid)
            {
                var dashboardViewData = ObjectContainer.Instance.RunQuery(
                            new DashboardViewDataQuery(filter.FromDate,
                                                filter.ToDate,
                                                filter.TaskId,
                                                filter.ScreenSize,
                                                filter.Path,
                                                filter.Language,
                                                filter.OperationSystem,
                                                filter.Country,
                                                filter.City,
                                                DataGrouping.Day));

                //Grouping data by day. To show on graph all days from start till end.
                var visitsData = new List<object[]>();
                int diffDays = (filter.ToDate - filter.FromDate).Days;
                for (int i = 0; i < diffDays; i++)
                {
                    int count = 0;
                    var curDate = filter.FromDate.AddDays(i);
                    if (dashboardViewData.Data.ContainsKey(curDate))
                    {
                        count = dashboardViewData.Data[curDate];
                    }
                    visitsData.Add(new object[] { curDate.MilliTimeStamp(), count });
                }

                //Create chart data
                var usageInitData = new List<object>();
                usageInitData.Add(new
                {
                    data = visitsData,
                    color = "#461D7C"
                });

                var dashboardModel = new DashboardModel(filter, AnalyticsMasterModel.MenuItem.Dashboard, dashboardViewData, false)
                {
                    UsageChartData = new JavaScriptSerializer().Serialize(usageInitData),
                    ContentOverviewData = dashboardViewData.ContentOverview.Select((d, i) => new ContentOverviewModel 
                                            { 
                                                ApplicationId = d.ApplicationId,
                                                ScreenId = d.ScreenId,
                                                Path = d.Path, 
                                                Views = d.Views, 
                                                Index = i 
                                            }).ToArray()
                };

                return View("~/Views/Analytics/Dashboard.cshtml", dashboardModel);
            }
            else
            {
                return Redirect("~/Error");
            }
        }

        public ActionResult Usage(FilterParametersModel filter)
        {
            if (ModelState.IsValid)
            {
                var query = new UsageViewDataQuery(
                filter.FromDate,
                filter.ToDate,
                filter.TaskId,
                filter.ScreenSize,
                filter.Path,
                filter.Language,
                filter.OperationSystem,
                filter.Country,
                filter.City,
                DataGrouping.Day);

                var usageViewData = ObjectContainer.Instance.RunQuery(query);

                //Grouping data by day. To show on graph all days from start till end.
                var data = new List<object[]>();
                int diffDays = (filter.ToDate - filter.FromDate).Days;
                for (int i = 0; i < diffDays; i++)
                {
                    int count = 0;
                    var curDate = filter.FromDate.AddDays(i);
                    if (usageViewData.Data.ContainsKey(curDate))
                    {
                        count = usageViewData.Data[curDate];
                    }
                    data.Add(new object[] { curDate.MilliTimeStamp(), count });
                }

                //Create chart data
                var usageInitData = new List<object>();
                usageInitData.Add(new
                {
                    data = data,
                    color = "#461D7C"
                });

                var model = new UsageModel(filter, AnalyticsMasterModel.MenuItem.Dashboard, usageViewData, false)
                { 
                    UsageChartData = new JavaScriptSerializer().Serialize(usageInitData) 
                };

                return View("", model);
            }
            else
            {
                return Redirect("~/Error");
            }
        }

        public ActionResult TouchMap(FilterParametersModel filter)
        {
            if (ModelState.IsValid)
            {
                var data = ObjectContainer.Instance.RunQuery(new FingerPrintViewDataQuery(
                                     filter.FromDate,
                                     filter.ToDate,
                                     filter.TaskId,
                                     filter.ScreenSize,
                                     filter.Path,
                                     null,
                                     null,
                                     null,
                                     null));

                string placeHolderHTML = string.Empty;
                //if (filterData.ScreenId.HasValue)
                //{
                //    placeHolderHTML = string.Format("<a href=\"/Application/ScreenEdit/{0}/3\" class=\"link2 btn-screen\"><span><span>Update Screen</span></span></a>", filterData.ScreenId.Value);
                //}
                //else
                //{
                //    placeHolderHTML = string.Format("<a href=\"/Application/ScreenNew/{0}/{1}/{2}/{3}/3\" class=\"link2 btn-screen\"><span><span>Add Screen</span></span></a>", filter.ApplicationId.Value, filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, HttpUtility.UrlEncode(filter.Path));
                //}
                if (data.ScreenData != null && data.ScreenData.Id.HasValue)
                {
                    placeHolderHTML = string.Format("<a href=\"/Application/ScreenEdit/{0}\" class=\"link2 btn-screen\"><span><span>Update Screen</span></span></a>", data.ScreenData.Id.Value);
                }
                else
                {
                    placeHolderHTML = string.Format("<a href=\"/Application/ScreenNew/{0}\" class=\"link2 btn-screen\"><span><span>Add Screen</span></span></a>", filter.TaskId);
                }

                //Grouping data by day. To show on graph all days from start till end.
                var visitsData = new List<object[]>();
                var clicksData = new List<object[]>();
                var scrollsData = new List<object[]>();
                int diffDays = (filter.ToDate - filter.FromDate).Days;

                var scroollValues = data.ScrollsData.Keys;
                


                for (int i = 0; i < diffDays; i++)
                {

                    var curDate = filter.FromDate.AddDays(i);

                    var scrollsKeys = data.ScrollsData.Where(s => s.Key.Year == curDate.Year
                                            && s.Key.DayOfYear == curDate.DayOfYear).Select(s => s.Key).ToList();
                    var count = data.ScrollsData.Where(s => scrollsKeys.Contains(s.Key)).Sum(g => g.Value);

          
                    visitsData.Add(new object[] { curDate.MilliTimeStamp(), data.VisitsData.ContainsKey(curDate) ? data.VisitsData[curDate] : 0 });
                    clicksData.Add(new object[] { curDate.MilliTimeStamp(), data.ClicksData.ContainsKey(curDate) ? data.ClicksData[curDate] : 0 });
                    scrollsData.Add(new object[] { curDate.MilliTimeStamp(),count });
                    

                }

            
                //Create chart data
                var graphsInitData = new 
                {
                    visits = new object[]
                    { 
                        new
                        {
                            data = visitsData,
                            color = "#461D7C"
                        }
                    },
                    clicks = new object[]
                    { 
                        new
                        {
                            data = clicksData,
                            color = "#461D7C"
                        }
                    },
                    scrolls = new object[]
                    { 
                        new
                        {
                            data = scrollsData,
                            color = "#461D7C"
                        }
                    }
                };

                var model = new FingerPrintModel(filter, AnalyticsMasterModel.MenuItem.TouchMap, data, true)
                {
                    // Title = "Fingerprint",
                    Screens = data.Screens,
                    GraphsData = new JavaScriptSerializer().Serialize(graphsInitData),
                    VisitsAmount = data.VisitsData.Sum(x => x.Value),
                    ControlClicks = data.ControlClicksData

                };

                return View("~/Views/Analytics/TouchMap.cshtml", model);
            }
            else
            {
                return Redirect("~/Error");
            }
       }

        public ActionResult ABCompare(ABFilterParametersModel filter)
        {
            if (ModelState.IsValid)
            {
                var filterData = ObjectContainer.Instance.RunQuery(new ABCompareViewDataQuery(
                                     filter.FromDate,
                                     filter.ToDate,
                                     filter.TaskId,
                                     filter.ScreenSize,
                                     filter.Path,
                                     filter.SecondPath,
                                     null,
                                     null,
                                     null,
                                     null));

                string placeHolderHTML = string.Empty;
                //if (filterData.ScreenId.HasValue)
                //{
                //    placeHolderHTML = string.Format("<a href=\"/Application/ScreenEdit/{0}/3\" class=\"link2 btn-screen\"><span><span>Update Screen</span></span></a>", filterData.ScreenId.Value);
                //}
                //else
                //{
                //    placeHolderHTML = string.Format("<a href=\"/Application/ScreenNew/{0}/{1}/{2}/{3}/3\" class=\"link2 btn-screen\"><span><span>Add Screen</span></span></a>", filter.ApplicationId.Value, filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, HttpUtility.UrlEncode(filter.Path));
                //}
                if (filterData.ScreenData != null && filterData.ScreenData.Id.HasValue)
                {
                    placeHolderHTML = string.Format("<a href=\"/Application/ScreenEdit/{0}\" class=\"link2 btn-screen\"><span><span>Update Screen</span></span></a>", filterData.ScreenData.Id.Value);
                }
                else
                {
                    placeHolderHTML = string.Format("<a href=\"/Application/ScreenNew/{0}\" class=\"link2 btn-screen\"><span><span>Add Screen</span></span></a>", filter.TaskId);
                }

                var pathes = filterData.Applications.Single(x => x.Id == filter.TaskId).Pathes;

                var firstScreenPathes = pathes.Select(x => new SelectListItem { Text = x, Value = x, Selected = string.IsNullOrEmpty(filter.Path) ? false : filter.Path == x });
                var secondScreenPathes = pathes.Select(x => new SelectListItem { Text = x, Value = x, Selected = string.IsNullOrEmpty(filter.Path) ? false : filter.SecondPath == x });

                string firstScreenPath = string.Empty;
                string secondScreenPath = string.Empty;
                int clicks = 0;
                int firstClicksData = 0;
                int secondClicksData = 0;
                int scrolls = 0;
                int firstScrollsData = 0;
                int secondScrollsData = 0;
                var visits = 0;
                int firstVisitsData = 0;
                int secondVisitsData = 0;
                if (firstScreenPathes.Any())
                {
                    firstScreenPath = firstScreenPathes.Any(x => x.Selected) ? firstScreenPathes.First(x => x.Selected).Text : firstScreenPathes.First().Text;
                    secondScreenPath = secondScreenPathes.Any(x => x.Selected) ? secondScreenPathes.First(x => x.Selected).Text : secondScreenPathes.First().Text;
                }

                if (filterData.ScreenData != null)
                {
                    clicks = filterData.ScreenData.ClicksAmount + filterData.SecondFilteredClicks;
                    firstClicksData = clicks > 0 ? filterData.ScreenData.ClicksAmount * 100 / clicks : 0;
                    secondClicksData = 100 - firstClicksData;
                    scrolls = filterData.ScreenData.ScrollsAmount + filterData.SecondFilteredScrolls;
                    firstScrollsData = scrolls > 0 ? filterData.ScreenData.ScrollsAmount * 100 / scrolls : 0;
                    secondScrollsData = 100 - firstScrollsData;
                    visits = filterData.ScreenData.VisitsAmount + filterData.SecondFilteredVisits;
                    firstVisitsData = visits > 0 ? filterData.ScreenData.VisitsAmount * 100 / visits : 0;
                    secondVisitsData = 100 - firstScrollsData;
                }
                //Create chart data
                var pieData = new
                {
                    clicks = new []
                    {
                        new {
                            label = secondScreenPath,
                            data = secondClicksData,
                            color = "#5182bd"
                        },
                        new {
                            label = firstScreenPath,
                            data = firstClicksData,
                            color = "#c0504d"
                        }         
                    },
                    scrolls = new[]
                    {
                        new {
                            label = filterData.SelectedSecondPath,
                            data = secondScrollsData,
                            color = "#5182bd"
                        },
                        new {
                            label = filterData.SelectedPath,
                            data = firstScrollsData,
                            color = "#c0504d"
                        }          
                    },
                    visits = new[]
                    {
                        new {
                            label = filterData.SelectedSecondPath,
                            data = secondVisitsData,
                            color = "#5182bd"
                        },
                        new {
                            label = filterData.SelectedPath,
                            data = firstVisitsData,
                            color = "#c0504d"
                        }          
                    }
                };

                ViewData["PieData"] = new JavaScriptSerializer().Serialize(pieData);

                var model = new ABCompareModel(filter, AnalyticsMasterModel.MenuItem.ABCompare, filterData, false/*, placeHolderHTML*/)
                {
                    // Title = "Fingerprint",
                    FirstScreenPathes = firstScreenPathes,
                    SecondScreenPathes = secondScreenPathes,
                    FirstPath = filter.Path,
                    SecondPath = filter.SecondPath,
                    FirstHasFilteredClicks = filterData.ScreenData != null && filterData.ScreenData.ClicksAmount > 0,
                    SecondHasFilteredClicks = filterData.SecondFilteredClicks > 0,
                    SecondHasClicks = filterData.SecondHasClicks,
                    FirstHasClicks = filterData.ScreenData != null && filterData.ScreenData.HasClicks
                };

                return View("~/Views/Analytics/ABCompare.cshtml", model);
            }
            else
            {
                return Redirect("~/Error");
            }
        }

        public ActionResult EyeTracker(FilterParametersModel filter)
        {
            if (ModelState.IsValid)
            {

                var data = ObjectContainer.Instance.RunQuery(new EyeTrackerViewDataQuery(
                                    filter.FromDate,
                                    filter.ToDate,
                                    filter.TaskId,
                                    filter.ScreenSize,
                                    filter.Path,
                                    null,
                                    null,
                                    null,
                                    null));

                string placeHolderHTML = string.Empty;
                //if (filterData.ScreenId.HasValue)
                //{
                //    placeHolderHTML = string.Format("<a href=\"/Application/ScreenEdit/{0}?returl={1}\" class=\"link2 btn-screen\"><span><span>Update Screen</span></span></a>", filterData.ScreenId.Value);
                //}
                //else
                //{
                //    placeHolderHTML = string.Format("<a href=\"/Application/ScreenNew/{0}/{1}/{2}/{3}?returl={4}\" class=\"link2 btn-screen\"><span><span>Add Screen</span></span></a>", filter.ApplicationId.Value, filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, HttpUtility.UrlEncode(filter.Path), HttpUtility.UrlEncode("/Analytics/FingerPrint/?pid=2&fd=06-Aug-2012&td=05-Sep-2012&aid=5&ss=480X800&p=Some View"));
                //}
                if (data.ScreenData != null && data.ScreenData.Id.HasValue)
                {
                    placeHolderHTML = string.Format("<a href=\"/Application/ScreenEdit/{0}\" class=\"link2 btn-screen\"><span><span>Update Screen</span></span></a>", data.ScreenData.Id.Value);
                }
                else
                {
                    placeHolderHTML = string.Format("<a href=\"/Application/ScreenNew/{0}\" class=\"link2 btn-screen\"><span><span>Add Screen</span></span></a>", filter.TaskId);
                }

                //Grouping data by day. To show on graph all days from start till end.
                var visitsData = new List<object[]>();
                int diffDays = (filter.ToDate - filter.FromDate).Days;
                for (int i = 0; i < diffDays; i++)
                {
                    int count = 0;
                    var curDate = filter.FromDate.AddDays(i);
                    if (data.UsageData.ContainsKey(curDate))
                    {
                        count = data.UsageData[curDate];
                    }
                    visitsData.Add(new object[] { curDate.MilliTimeStamp(), count });
                }

                //Create chart data
                var usageInitData = new List<object>();
                usageInitData.Add(new
                {
                    data = visitsData,
                    color = "#461D7C"
                });

                var model = new EyeTrackerModel(filter, AnalyticsMasterModel.MenuItem.EyeTracker, data, false)
                {
                    // Title = "Eye Tracker",
                    Screens = data.Screens,
                    UsageChartData = new JavaScriptSerializer().Serialize(usageInitData)
                };

                return View("~/Views/Analytics/EyeTracker.cshtml", model);
            }
            else
            {
                return Redirect("~/Error");
            }
        }

        public FileResult ClickHeatMapImage(FilterParametersModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Path))
            {
                throw new HttpException(400, "Parameter Path was not supplied");
            }

            if (!filter.ScreenSize.HasValue)
            {
                throw new HttpException(400, "Parameter ScreenSize was not supplied");
            }

            var result = ObjectContainer.Instance.RunQuery(new ClickHeatMapDataQuery(filter.TaskId, filter.Path, filter.ScreenSize.Value, filter.FromDate, filter.ToDate));

            byte[] imageData = null;
            Image image = null;
            if (result.Data.Any())
            {
                Image bgImg = GetBackgroundImage(result.Screen);
                if (string.IsNullOrEmpty(Request["cscreen"]))
                {
                    if (bgImg == null)
                    {
                        bgImg = HeatMapImage_.CreateEmpityBackground("NO IMAGE", filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height);
                    }

                    image = new HeatMapImage().CreateClicksHeatMap((Bitmap)bgImg, result.Data.Select(x => new IntensityPoint() { X = x.ClientX, Y = x.ClientY, Intensity = x.Count }).ToList());
                }
                else
                {
                    image = bgImg;
                }
            }
            else
            {
                //Show no data
                image = HeatMapImage_.CreateEmpityBackground("NO DATA", filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, GetBackgroundImage(result.Screen));
            }

            if (image != null)
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Save(mStream, ImageFormat.Png);
                    imageData = mStream.ToArray();
                }
                image.Dispose();
            }

            if (imageData == null)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                return base.File(imageData, "Image/png");
            }
        }

        public FileResult ViewHeatMapImage(FilterParametersModel filter)
        {
            var result = ObjectContainer.Instance.RunQuery(new HeatMapDataQuery(filter.TaskId, filter.Path, filter.ScreenSize.Value, filter.FromDate, filter.ToDate));
            
            byte[] imageData = null;
            Image image = null;

            if (result.Data.Any())
            {
                Image bgImg = GetBackgroundImage(result.Screen);
                if (string.IsNullOrEmpty(Request["cscreen"]))
                {
                    image = HeatMapImage_.CreateViewHeatMap(result.Data, filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, bgImg);
                }
                else
                {
                    image = bgImg;
                }
            }
            else
            {
                //Show no data
                image = HeatMapImage_.CreateEmpityBackground("NO DATA", filter.ScreenSize.Value.Width, filter.ScreenSize.Value.Height, GetBackgroundImage(result.Screen));
            }

            if (image != null)
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Save(mStream, ImageFormat.Png);
                    imageData = mStream.ToArray();
                }
            }

            if (imageData == null)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                return base.File(imageData, "Image/png");
            }
        }

        private Image GetBackgroundImage(ScreenResult screen)
        {
            Image bgImg = null;
            if (screen != null)
            {
                string bgPath = Path.Combine(Server.MapPath("~/Restricted/Screens/"), string.Concat(screen.Id, screen.FileExtension));
                if (System.IO.File.Exists(bgPath)) bgImg = Image.FromFile(bgPath);
            }
            return bgImg;
        }
    }
}
