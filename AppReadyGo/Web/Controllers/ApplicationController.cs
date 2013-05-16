using AppReadyGo.Common;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands.Application;
using AppReadyGo.Core.Entities;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Application;
using AppReadyGo.Model.Pages.Portfolio;
using AppReadyGo.Web.Model.Pages.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(string srch = "", int scol = 1, int cp = 1)
        {
            if (ObjectContainer.Instance.CurrentUserDetails.Type == UserType.Staff)
            {
                return RedirectToAction("", "Admin");
            }

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
                    Scrolls = rnd.Next(1000),
                    Clicks = rnd.Next(1000),
                    Time = rnd.Next(100),
                    TargetGroup = rnd.Next(100) > 50 ? "Men 18+" : "Women 18+",
                    Description = a.Description,
                    Icon = string.IsNullOrEmpty(a.IconExt) ?  "/content/images/no_icon.png" : string.Format("/Restricted/Icons/{0}{1}", a.Id, a.IconExt),
                    Published = a.Published,
                    Downloaded = a.Downloaded,
                    HasScreens = a.HasScreens
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

        public static IEnumerable<T> GetList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
