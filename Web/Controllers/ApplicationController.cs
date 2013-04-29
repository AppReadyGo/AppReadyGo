using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands.Application;
using AppReadyGo.Core.Entities;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults.Application;
using AppReadyGo.Controllers.Master;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Application;
using AppReadyGo.Model.Pages.Portfolio;
using AppReadyGo.Common;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Web.Model.Pages.Application;

namespace AppReadyGo.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.Analytics; }
        }
        public ActionResult Index(string srch = "", int scol = 1, int cp = 1)
        {
            var data = ObjectContainer.Instance.RunQuery(new GetAllApplicationsQuery(srch, cp, 15));

            if (!data.Applications.Any())
            {
                return RedirectToAction("New");
            }

            ViewData["IsAdmin"] = User.IsInRole(StaffRole.Administrator.ToString());

            var rnd = new Random();

            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));
            var model = new PortfolioIndexModelTmp(AfterLoginMasterModel.MenuItem.Analytics)
            {
                IsOnePage = data.TotalPages == 1,
                Count = data.Count,
                PreviousPage = data.CurPage == 1 ? null : (int?)(data.CurPage - 1),
                NextPage = data.CurPage == data.TotalPages ? null : (int?)(data.CurPage + 1),
                TotalPages = data.TotalPages,
                CurPage = data.CurPage,
                SearchStrUrlPart = searchStrUrlPart,
                SearchStr = srch,
                Applications = data.Applications.Select((a, i) => new ApplicationItemModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsActive = a.IsActive,
                    Alternate = i % 2 != 0,
                    Visits = a.Visits,
                    Key = a.Id.GetAppKey(),
                    Downloads = rnd.Next(100),
                    PublishedDate = DateTime.Now.AddDays(-rnd.Next(100)).ToString("dd MMM yyyy"),
                    Scrolls = rnd.Next(1000),
                    Clicks = rnd.Next(1000),
                    Time = rnd.Next(100),
                    TargetGroup = rnd.Next(100) > 50 ? "Men 18+" : "Women 18+",
                    Description = a.Description,
                    Icon = string.IsNullOrEmpty(a.IconExt) ?  "/content/images/no_icon.png" : string.Format("/Restricted/Icons/{0}{1}", a.Id, a.IconExt),
                    Published = a.Published,
                }).ToArray(),
                TopApplications = data.TopApplications.Select((a, i) => new TopApplicationsItemModel
                {
                    IsAlternative = i % 2 != 0,
                    Id = a.Id,
                    Description = a.Name
                }).ToArray(),
                TopScreens = data.TopScreens.Select((s, i) => new TopScreensItemModel
                {
                    IsAlternative = i % 2 != 0,
                    ApplicationId = s.ApplicationId,
                    ScreenSize = s.ScreenSize.ToFormatedString(),
                    Path = s.Path
                }).ToArray()
            };
            return View("~/Views/Application/Index.cshtml", model);
        }

        public ActionResult New()
        {
            var appTypes = ObjectContainer.Instance.RunQuery(new GetApplicationTypesQuery());
            var types = appTypes.Select(x => new SelectListItem() { Text = x.Item2, Value = x.Item1.ToString() });
            return View(new ApplicationModel { Types = types });
        }

        [HttpPost]
        public ActionResult New(ApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                string iconExt = Request.Files.AllKeys.Contains("icon_file") ? Path.GetExtension(Request.Files[0].FileName) : null;
                var result = ObjectContainer.Instance.Dispatch(new CreateApplicationCommand(model.Name, model.Description, model.Type, iconExt));

                if (result.Validation.Any())
                {
                    log.WriteError("Error to add application to database", string.Join("; ", result.Validation.Select(v => string.Format("Code:{0}, Message: {1}", v.ErrorCode, v.Message)).ToArray()));
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    if (!string.IsNullOrEmpty(iconExt))
                    {
                        var path = Path.Combine(Server.MapPath("~/Restricted/Icons/"), result.Result + iconExt);
                        log.WriteInformation("Save file: {0}", path);
                        Request.Files[0].SaveAs(path);
                    }

                    var keys = Request.Files.AllKeys.Where(x => x.Contains("screen_file_"));
                    foreach (var key in keys)
                    {
                        var ext = Path.GetExtension(Request.Files[key].FileName);
                        var sres = ObjectContainer.Instance.Dispatch(new AddScreenshotCommand(result.Result, ext));
                        var path = Path.Combine(Server.MapPath("~/Restricted/Screenshots/"), sres.Result + ext);
                        if (!sres.Validation.Any())
                        {
                            log.WriteInformation("Save file: {0}", path);
                            Request.Files[key].SaveAs(path);
                        }
                    }
                }

                return RedirectToAction("");
            }
            else
            {
                var appTypes = ObjectContainer.Instance.RunQuery(new GetApplicationTypesQuery());
                var types = appTypes.Select(x => new SelectListItem() { Text = x.Item2, Value = x.Item1.ToString() });
                model.Types = types;
                return View("New", model);
            }
        }


        public ActionResult Edit(int id)
        {
            var res = ObjectContainer.Instance.RunQuery(new GetApplicationEditDataQuery(id));
            var model = new ApplicationEditModel()
            {
                Id = res.ApplicationDetails.Id,
                Name = res.ApplicationDetails.Name,
                Description = res.ApplicationDetails.Description,
                Type = res.ApplicationDetails.Type.Item1,
                Types = res.ApplicationTypes.Select(x => new SelectListItem() { Text = x.Item2, Value = x.Item1.ToString() }),
                IconPath = string.IsNullOrEmpty(res.ApplicationDetails.IconExt) ? "/content/images/no_icon.png" : string.Format("/Restricted/Icons/{0}{1}", res.ApplicationDetails.Id, res.ApplicationDetails.IconExt),
                ScreensPathes = res.Screenshots.Select(s => string.Format("/Restricted/Screenshots/{0}{1}", s.Item1, s.Item2))
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                var result = ObjectContainer.Instance.Dispatch(new UpdateApplicationCommand(model.Id, model.Name, model.Description, model.Type));

                if (result.Validation.Any())
                {
                    log.WriteError("Error to add application to database", string.Join("; ", result.Validation.Select(v => string.Format("Code:{0}, Message: {1}", v.ErrorCode, v.Message)).ToArray()));
                    return RedirectToAction("Error", "Home");
                }
                else
                {
                    string iconExt = Request.Files.AllKeys.Contains("icon_file") ? Path.GetExtension(Request.Files[0].FileName) : null;
                    if (!string.IsNullOrEmpty(iconExt))
                    {
                        var updateExtRes = ObjectContainer.Instance.Dispatch(new UpdateApplicationIconCommand(model.Id, iconExt));
                        if (!string.IsNullOrEmpty(updateExtRes.Result))
                        {
                            // Delete old icon
                            var oldPath = Path.Combine(Server.MapPath("~/Restricted/Icons/"), model.Id + updateExtRes.Result);
                            log.WriteInformation("Delete file: {0}", oldPath);
                            System.IO.File.Delete(oldPath);
                        }
                        //Save new icon
                        var path = Path.Combine(Server.MapPath("~/Restricted/Icons/"), model.Id + iconExt);
                        log.WriteInformation("Save file: {0}", path);
                        Request.Files[0].SaveAs(path);
                    }

                    var keys = Request.Files.AllKeys.Where(x => x.Contains("screen_file_"));
                    foreach (var key in keys)
                    {
                        var ext = Path.GetExtension(Request.Files[key].FileName);
                        var sres = ObjectContainer.Instance.Dispatch(new AddScreenshotCommand(result.Result, ext));
                        var path = Path.Combine(Server.MapPath("~/Restricted/Screenshots/"), sres.Result + ext);
                        if (!sres.Validation.Any())
                        {
                            log.WriteInformation("Save file: {0}", path);
                            Request.Files[key].SaveAs(path);
                        }
                    }
                }
                return RedirectToAction("");
            }
            else
            {
                ViewBag.Edit = true;
                ViewBag.Version = ContentPredefinedKeys.AndroidPackageVersion.GetContent();
                return View(model);
            }
        }

        public ActionResult Remove(int id)
        {
            var app = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(id));
            if (app == null)
            {
                return View("Error");
            }
            else
            {
                // TODO: remove all screens, screenshots and icon files
                ObjectContainer.Instance.Dispatch(new RemoveApplicationCommand(id));
            }
            return Redirect("/Application");
        }

        public ActionResult Publish(int id)
        {
            var res = ObjectContainer.Instance.RunQuery(new PublishQuery(id));
            var countries = res.Countries.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
            countries.Insert(0, new SelectListItem { Text = "All", Value = "" });
            var genders = new SelectListItem[] { new SelectListItem { Value = "", Text = "All" }, new SelectListItem { Value = "1", Text = "Men" }, new SelectListItem { Value = "2", Text = "Women" } };
            var ageRanges = GetList<AgeRange>().Select(x => new SelectListItem { Value = ((int)x).ToString(), Text = x.ToString() }).ToList();
            ageRanges.Insert(0, new SelectListItem { Text = "All", Value = "" });
            var model = new PublishModel
            {
                ApplicationId = id,
                ApplicationName = res.ApplicationName,
                Countries = countries,
                Genders = genders,
                AgeRanges = ageRanges
            };
            return View("~/Views/Application/Publish.cshtml", model);
        }

        [HttpPost]
        public ActionResult Publish(PublishModel model)
        {
            if (!string.IsNullOrEmpty(model.Zip) && !model.Country.HasValue)
            {
                ModelState.AddModelError("Country", "Please select a country when zip entered.");
            }

            if (ModelState.IsValid)
            {
                ObjectContainer.Instance.Dispatch(new PublishCommand(model.ApplicationId, model.AgeRange, model.Gender, model.Country, model.Zip));
                return RedirectToAction("", "Application");
            }
            else
            {
                var res = ObjectContainer.Instance.RunQuery(new PublishQuery(model.ApplicationId));
                var countries = res.Countries.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
                countries.Insert(0, new SelectListItem { Text = "All", Value = "" });
                var genders = new SelectListItem[] { new SelectListItem { Value = "", Text = "All" }, new SelectListItem { Value = "1", Text = "Men" }, new SelectListItem { Value = "2", Text = "Women" } };
                var ageRanges = GetList<AgeRange>().Select(x => new SelectListItem { Value = ((int)x).ToString(), Text = x.ToString() }).ToList();
                ageRanges.Insert(0, new SelectListItem { Text = "All", Value = "" });
                model.ApplicationName = res.ApplicationName;
                model.Countries = countries;
                model.Genders = genders;
                model.AgeRanges = ageRanges;
                return View("~/Views/Application/Publish.cshtml", model);
            }
        }

        public ActionResult Publishes(int id)
        {
            var res = ObjectContainer.Instance.RunQuery(new GetPublishesDataQuery(id));
            var model = new PublishesModel
            {
                ApplicationId = id,
                ApplicationName = res.ApplicationName,
                Publishes = res.PublishesDetails.Select(x => new PublishDetailsModel
                {

                    Id = x.Id,
                    CreatedDate = x.CreatedDate.ToString("dd-MMM-yyyy"),
                    Country = x.Country == null ? string.Empty : x.Country.Item2,
                    AgeRange = x.AgeRange.HasValue ? x.AgeRange.Value.GetName() : "All",
                    Gender = x.Gender.HasValue ? x.Gender.Value.ToString() : "All",
                    Zip = x.Zip
                })
            };
            return View(model);
        }

        public ActionResult Unpublish(int id)
        {
            var appId = ObjectContainer.Instance.Dispatch(new UnPublishCommand(id));
            return RedirectToAction("Publishes", new { id = appId.Result });
        }

        public ActionResult Screens(int id, string srch = "", int scol = 1, int cp = 1, string orderby = "", string order = "")
        {
            var orderBy = string.IsNullOrEmpty(orderby) ? ScreensQuery.OrderByColumn.Path : (ScreensQuery.OrderByColumn)Enum.Parse(typeof(ScreensQuery.OrderByColumn), orderby, true);
            bool asc = string.IsNullOrEmpty(orderby) ? ((orderBy == ScreensQuery.OrderByColumn.Path) ? false : true) : order.Equals("asc", StringComparison.OrdinalIgnoreCase);
            var data = ObjectContainer.Instance.RunQuery(new ScreensQuery(id, srch, cp, 10, orderBy, asc));
            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));

            var model = new ScreensListModel
            {
                Pagging = new Model.PagingModel
                {
                    IsOnePage = data.TotalPages == 1,
                    Count = data.Count,
                    PreviousPage = data.CurPage == 1 ? null : (int?)(data.CurPage - 1),
                    NextPage = data.CurPage == data.TotalPages ? null : (int?)(data.CurPage + 1),
                    TotalPages = data.TotalPages,
                    CurPage = data.CurPage,
                    SearchStrUrlPart = searchStrUrlPart,
                    SearchStr = srch,
                    UrlPart = string.Concat(searchStrUrlPart, string.IsNullOrEmpty(orderby) ? string.Empty : string.Concat("&orderby=", orderby), string.IsNullOrEmpty(order) ? string.Empty : string.Concat("&order=", order)),
                },


                PathOrder = orderBy == ScreensQuery.OrderByColumn.Path && asc ? "desc" : "asc",
                WidthOrder = orderBy == ScreensQuery.OrderByColumn.Width && asc ? "desc" : "asc",
                HeightOrder = orderBy == ScreensQuery.OrderByColumn.Height && asc ? "desc" : "asc",

                ApplicationId = data.ApplicationId,
                ApplicationDescription = data.ApplicationDescription,
                Screens = data.Screens.Select((s, i) => new ScreenItemModel
                {
                    Id = s.Id,
                    Width = s.Width,
                    Height = s.Height,
                    Path = s.Path,
                    FileExtention = s.FileExtension,
                    IsAlternative = i % 2 != 0
                }).ToArray()
            };
            return View(model);
        }

        //public ActionResult ScreenNew(int id, int width, int height, string path, ScreenReturn ret)
        //{
        //    switch (ret)
        //    {
        //        case ScreenReturn.EyeTracker:
        //            ViewBag.ReturnUrl = string.Format("/Analytics/EyeTracker/?pid={0}&aid={1}&ss={2}&p={3}", 1, id, new Size(width, height).ToFormatedString(), path);
        //            break;
        //        case ScreenReturn.FingerPrint:
        //            ViewBag.ReturnUrl = string.Format("/Analytics/TouchMap/?pid={0}&aid={1}&ss={2}&p={3}", 1, id, new Size(width, height).ToFormatedString(), path);
        //            break;
        //        default:
        //            ViewBag.ReturnUrl = Redirect("/Application/Screens/" + id);
        //            break;
        //    }
        //    return GetActionData(id, new ScreenModel { ApplicationId = id, Height = height, Width = width, Path = HttpUtility.UrlDecode(path), ScreenReturn = ret });
        //}
        public ActionResult ScreenNew(int id)
        {
            return GetActionData(id, new ScreenModel { ApplicationId = id });
        }


        [HttpPost]
        public ActionResult ScreenNew(ScreenModel model)
        {
            log.WriteInformation("Add a new screen for application: {0}, Height: {1}, Width: {2}", model.ApplicationId, model.Height, model.Width);
            if (model.Height <= 0)
            {
                ModelState.AddModelError("Height", "Please enter correct height.");
            }
            if (model.Width <= 0)
            {
                ModelState.AddModelError("Width", "Please enter correct width.");
            }
            if(Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("file", "The File field is required.");
            }
            if (ModelState.IsValid)
            {
                string fileExtention = Path.GetExtension(Request.Files[0].FileName);

                var result = ObjectContainer.Instance.Dispatch(new AddScreenCommand(model.ApplicationId, model.Path, model.Width, model.Height, fileExtention));
                if (result.Validation.Any())
                {
                    log.WriteError("Error to add screen to database: {0}", string.Join("; ", result.Validation.Select(v => string.Format("Code:{0}, Message: {1}", v.ErrorCode, v.Message)).ToArray()));
                    return View("Error");
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), result.Result + fileExtention);
                    log.WriteInformation("Save file: {0}", path);
                    Request.Files[0].SaveAs(path);
                }

                return Redirect("/Application/Screens/" + model.ApplicationId);

                //switch (model.ScreenReturn)
                //{
                //    case ScreenReturn.EyeTracker:
                //        return Redirect(string.Format("/Analytics/EyeTracker/?pid={0}&aid={1}&ss={2}&p={3}", 1, model.ApplicationId, new Size(model.Width, model.Height).ToFormatedString(), model.Path));
                //    case ScreenReturn.FingerPrint:
                //        return Redirect(string.Format("/Analytics/TouchMap/?pid={0}&aid={1}&ss={2}&p={3}", 1, model.ApplicationId, new Size(model.Width, model.Height).ToFormatedString(), model.Path));
                //    default:
                //        return Redirect("/Application/Screens/" + model.ApplicationId);
                //}
            }
            else
            {
                log.WriteInformation("Wrong model");
                return GetActionData(model.ApplicationId, model);
            }
        }

        private ActionResult GetActionData(int id, ScreenModel model)
        {
            var data = ObjectContainer.Instance.RunQuery(new GetScreenDataQuery(id));

            return PrepareAction(model, data);
        }

        private ActionResult PrepareAction(ScreenModel model, ScreenDataResult data)
        {
            ViewBag.ApplicationDescription = data.ApplicationDescription;

            var pathes = data.Pathes.Select(p => new SelectListItem { Text = p, Value = p }).ToList();
            pathes.Insert(0, new SelectListItem { Text = "Select from exists", Value = "" });
            ViewData["predefinedPathes"] = pathes;

            var sizes = data.Sizes.Select(s => new SelectListItem { Text = s.Width + "X" + s.Height, Value = s.Width + "X" + s.Height }).ToList();
            sizes.Insert(0, new SelectListItem { Text = "Select from exists", Value = "X" });
            ViewData["predefinedSizes"] = sizes;

            return View(model);
        }

        public ActionResult ScreenEdit(int id/*, ScreenReturn ret*/)
        {
            var data = ObjectContainer.Instance.RunQuery(new GetScreenEditDataQuery(id));
            if (data == null)
            {
                return View("Error");
            }
            else
            {
                var model = new ScreenModel
                {
                    Id = data.Id,
                    Path = data.Path,
                    Width = data.Width,
                    Height = data.Height,
                    FileExtention = data.FileExtention,
                    ApplicationId = data.ApplicationId
                };
                return PrepareAction(model, data);
            }
        }

        [HttpPost]
        public ActionResult ScreenEdit(ScreenModel model)
        {
            if (model.Height <= 0)
            {
                ModelState.AddModelError("Height", "Please enter correct height.");
            }
            if (model.Width <= 0)
            {
                ModelState.AddModelError("Width", "Please enter correct width.");
            }
            if (ModelState.IsValid)
            {
                bool newFile = Request.Files.Count == 1 && Request.Files[0].ContentLength > 0;
                string fileExtention = newFile ? Path.GetExtension(Request.Files[0].FileName) : model.FileExtention;

                var result = ObjectContainer.Instance.Dispatch(new UpdateScreenCommand(model.Id, model.Path, model.Width, model.Height, fileExtention));
                if (result.Validation.Any())
                {
                    return View("Error");
                }
                else
                {
                    if (newFile)
                    {
                        //Remove previous file
                        var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), model.Id + model.FileExtention);
                        System.IO.File.Delete(path);

                        path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), model.Id + fileExtention);
                        Request.Files[0].SaveAs(path);
                    }
                }
                return Redirect("/Application/Screens/" + model.ApplicationId.ToString());
            }
            else
            {
                return GetActionData(model.ApplicationId, model);
            }
        }

        public ActionResult ScreenRemove(int id)
        {
            var screen = ObjectContainer.Instance.RunQuery(new GetScreenDetailsQuery(id));
            if (screen == null)
            {
                return View("Error");
            }
            else
            {
                ObjectContainer.Instance.Dispatch(new RemoveScreenCommand(id));
                var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), screen.Id + screen.FileExtention);
                System.IO.File.Delete(path);
            }
            return Redirect("/Application/Screens/" + screen.ApplicationId);
        }

        /*
        public ActionResult Index(int portfolioId)
        {
            var appRes = service.GetAll(portfolioId);
            if (appRes.HasError)
            {
                return View("Error");
            }

            ViewBag.PortfolioId = portfolioId;
            var columnHeaders = new List<HTMLTable.Cell>() {
                    new HTMLTable.Cell() { Value = "Description" }, 
                    new HTMLTable.Cell() { Value = "Type" }, 
                    new HTMLTable.Cell() { Value = "% Change" },
                    new HTMLTable.Cell() { Value = "" } 
                };
            var data = new List<List<HTMLTable.Cell>>();

            if (appRes.Value.Count > 0)
            {
                //Create table
                foreach (var curApp in appRes.Value)
                {
                    var cells = new List<HTMLTable.Cell>();
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Analytics/Application/Dashboard/{0}\">{1}</a>", curApp.Id, curApp.Description) });
                    cells.Add(new HTMLTable.Cell() { Value = curApp.Type.ToString() });
                    cells.Add(new HTMLTable.Cell() { Value = "0.00%" });
                    cells.Add(new HTMLTable.Cell() { Value = string.Format("<a href=\"/Application/Edit/{0}/{1}\">edit</a>&nbsp;<a href=\"/Application/Remove/{0}/{1}\">remove</a>", portfolioId, curApp.Id) });
                    data.Add(cells);
                }
            }
            else
            {
                data.Add(new List<HTMLTable.Cell>() { new HTMLTable.Cell() { ColSpan = 8, StyleClass = "no-data", Value = "No Applications" } });
            }

            ViewData["caption"] = new HTMLTable.Cell() { Value = "Accounts" };
            ViewData["columnHeaders"] = columnHeaders;
            ViewData["data"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult AddScreen(ScreenDetailsModel screenDetails)
        {
            object res = null;
            if (ModelState.IsValid)
            {
                var file = Request.Files["screen_img"];
                var screen = new Screen { 
                    ApplicationId = screenDetails.AppId,
                    Width = screenDetails.Width,
                    Height = screenDetails.Height,
                    FileExtension = Path.GetExtension(file.FileName)
                };
                var addRes = service.AddScreen(screen);
                if (!addRes.HasError)
                {
                    string tmpFileFullName = null;
                    if (file.ContentLength > 0)
                    {
                        tmpFileFullName = Path.Combine(HttpContext.Server.MapPath("/Users_Resources/Screens/"), string.Format("{0}_{1}X{2}{3}", screenDetails.AppId, screen.Width, screen.Height, screen.FileExtension));
                        file.SaveAs(tmpFileFullName);
                    }
                    res = new { HasError = false, ScreenId = addRes.Value };
                }
                else
                {
                    res = new { HasError = true, ScreenId = -1 };
                }
            }
            var actionResult = base.Json(res);
            actionResult.ContentType = "text/html";
            return actionResult;
        }

        public FileResult Screen(int appId, int width, int height, string file)
        {
            var res = service.GetScreen(appId, width, height);
            if (!res.HasError && res.Value != null)
            {
                string tmpFileFullName = Path.Combine(HttpContext.Server.MapPath("/Users_Resources/Screens/"), string.Format("{0}_{1}X{2}{3}", res.Value.ApplicationId, res.Value.Width, res.Value.Height, res.Value.FileExtension));
                return base.File(tmpFileFullName, MIMEAssistant.GetMIMEType(tmpFileFullName));
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        public ActionResult Dashboard(int portfolioId, int appId)
        {
            var points = new Dictionary<DateTime, int> { { DateTime.Now.AddDays(-5), 40 }, { DateTime.Now.AddDays(-4), 30 }, { DateTime.Now.AddDays(-3), 10 }, { DateTime.Now.AddDays(-2), 50 }, { DateTime.Now.AddDays(-1), 40 } };
            //Fill chart data
            var chartInitData = new List<object>();
            chartInitData.Add(new
            {
                data = points.OrderBy(curItem => curItem.Key).Select(curItem => new object[] { curItem.Key.MilliTimeStamp(), curItem.Value }),
                color = "#461D7C"
            });
            ViewBag.ChartInitData = new JavaScriptSerializer().Serialize(chartInitData);
            ViewBag.PortfolioId = portfolioId;
            ViewBag.ApplicationId = appId;
            return View();
        }

        public ActionResult EyeTracker(int portfolioId, int appId)
        {
            ViewBag.PortfolioId = portfolioId;
            ViewBag.ApplicationId = appId;

            DateTime fromDate = DateTime.UtcNow.AddDays(-30);
            DateTime toDate = DateTime.UtcNow;
            var res = service.GetEyeTrackerData(appId, fromDate, toDate);
            if (res.HasError)
            {
                return View("Error");
            }
            else
            {
                if (res.Value.PageUris.Count() == 0 || res.Value.ScreenSizes.Count() == 0)
                {
                    ViewBag.NoData = true;
                }
                else
                {
                    ViewBag.NoData = false;
                    ViewData["ScreenSizes"] = new List<SelectListItem>(res.Value.ScreenSizes.Select(s => new SelectListItem { Text = string.Format("{0} X {1}", s.Width, s.Height), Value = string.Format("{0}X{1}", s.Width, s.Height) }));
                    ViewData["PageUris"] = new List<SelectListItem>(res.Value.PageUris.Select(s => new SelectListItem { Text = s, Value = s }));
                    ViewBag.EyeTrackerImageUrl = string.Format("/Application/ViewHeatMapImage/{0}/?appId={0}&pageUri={1}&clientWidth={2}&clientHeight={3}&fromDate={4}&toDate={5}&preview=true", appId, HttpUtility.UrlEncode(res.Value.PageUris.First()), res.Value.ScreenSizes.First().Width, res.Value.ScreenSizes.First().Height, HttpUtility.UrlEncode(fromDate.ToString("HH:mm dd-MMM-yyyy")), HttpUtility.UrlEncode(toDate.ToString("HH:mm dd-MMM-yyyy")));
                }
                return View("Image");
            }
        }

        public ActionResult Fingerprint(int portfolioId, int appId)
        {
            ViewBag.PortfolioId = portfolioId;
            ViewBag.ApplicationId = appId;

            DateTime fromDate = DateTime.UtcNow.AddDays(-30);
            DateTime toDate = DateTime.UtcNow;
            var res = service.GetEyeTrackerData(appId, fromDate, toDate);
            if (res.HasError)
            {
                return View("Error");
            }
            else
            {
                if (res.Value.PageUris.Count() == 0 || res.Value.ScreenSizes.Count() == 0)
                {
                    ViewBag.NoData = true;
                }
                else
                {
                    ViewBag.NoData = false;
                    ViewData["ScreenSizes"] = new List<SelectListItem>(res.Value.ScreenSizes.Select(s => new SelectListItem { Text = string.Format("{0} X {1}", s.Width, s.Height), Value = string.Format("{0}X{1}", s.Width, s.Height) }));
                    ViewData["PageUris"] = new List<SelectListItem>(res.Value.PageUris.Select(s => new SelectListItem { Text = s, Value = s }));
                    ViewBag.EyeTrackerImageUrl = string.Format("/Application/ClickHeatMapImage/{0}/?appId={0}&pageUri={1}&clientWidth={2}&clientHeight={3}&fromDate={4}&toDate={5}&preview=true", appId, HttpUtility.UrlEncode(res.Value.PageUris.First()), res.Value.ScreenSizes.First().Width, res.Value.ScreenSizes.First().Height, HttpUtility.UrlEncode(fromDate.ToString("HH:mm dd-MMM-yyyy")), HttpUtility.UrlEncode(toDate.ToString("HH:mm dd-MMM-yyyy")));
                }
                return View("Image");
            }
        }
        */

        public static IEnumerable<T> GetList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
