using AppReadyGo.Common;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands.Applications;
using AppReadyGo.Core.Entities;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Applications;
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
using AppReadyGo.Web.Model.Shared;
using AppReadyGo.Core.Commands.Task;

namespace AppReadyGo.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string[] packageExtentions = new string[] { ".jar", ".apk" };

        public ActionResult Index()
        {
            var data = ObjectContainer.Instance.RunQuery(new GetApplicationIndexDataQuery());

            var model = new ApplicationIndexModel
            {
                Tasks = data.Tasks.Select((p, i) => new ApplicationIndexModel.TaskItem
                {
                    Id = p.Id,
                    Description = "Some Desc",
                    IsAlternative = i % 2 == 0
                }),
                Applications = data.Applications.Select((a, i) => new ApplicationIndexModel.ApplicationItem
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsAlternative = i % 2 == 0
                })
            };
            return View("~/Views/Application/Index.cshtml", model);
        }

        public ActionResult New()
        {
            var appTypes = ObjectContainer.Instance.RunQuery(new GetApplicationTypesQuery());
            var types = appTypes.Select(x => new SelectListItem() { Text = x.Item2, Value = x.Item1.ToString() }).OrderBy(x => x.Text);
            return View(new ApplicationModel { Types = types });
        }

        [HttpPost]
        public ActionResult New(ApplicationModel model)
        {
            var packageFile = Request.Files["package_file"];
            if (packageFile.ContentLength > 0)
            {
                var ext = Path.GetExtension(packageFile.FileName);
                if (!packageExtentions.Contains(ext))
                {
                    ModelState.AddModelError("Package", "The package file is wrong format.");
                }
                else if (packageFile.ContentLength > 52428800/*50MB*/)
                {
                    ModelState.AddModelError("Package", "The package file is too big, the maximum package size is 50MB.");
                }
            }

            if (ModelState.IsValid)
            {
                string iconExt = Request.Files.AllKeys.Contains("icon_file") ? Path.GetExtension(Request.Files["icon_file"].FileName) : null;
                var result = ObjectContainer.Instance.Dispatch(new CreateApplicationCommand(model.Name, model.Description, model.Type, iconExt));

                if (result.Validation.Any())
                {
                    if (result.Validation.Any(x => x.ErrorCode == ErrorCode.WrongDescription))
                    {
                        ModelState.AddModelError("Description", "The description is longer than 500 characters.");
                    }
                    else
                    {
                        log.WriteError("Error to add application to database", string.Join("; ", result.Validation.Select(v => string.Format("Code:{0}, Message: {1}", v.ErrorCode, v.Message)).ToArray()));
                        return Redirect("/Error");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(iconExt))
                    {
                        var path = Path.Combine(Server.MapPath("~/Restricted/Icons/"), result.Result + iconExt);
                        log.WriteInformation("Save file: {0}", path);
                        Request.Files["icon_file"].SaveAs(path);
                    }

                    var keys = Request.Files.AllKeys.Where(x => x.Contains("screen_file_"));
                    foreach (var key in keys)
                    {
                        var ext = Path.GetExtension(Request.Files[key].FileName);
                        // TODO: merge the command to one with create application
                        var sres = ObjectContainer.Instance.Dispatch(new AddScreenshotCommand(result.Result, ext));
                        var path = Path.Combine(Server.MapPath("~/Restricted/Screenshots/"), sres.Result + ext);
                        if (!sres.Validation.Any())
                        {
                            log.WriteInformation("Save file: {0}", path);
                            Request.Files[key].SaveAs(path);
                        }
                    }

                    if (packageFile.ContentLength > 0)
                    {
                        // TODO: merge the command to one with create application
                        ObjectContainer.Instance.Dispatch(new UpdatePackageCommand(result.Result, packageFile.FileName));
                        var path = Path.Combine(Server.MapPath("~/Restricted/UserPackages/"), result.Result.ToString());
                        packageFile.SaveAs(path);
                    }

                    return RedirectToAction("");
                }
            }

            var appTypes = ObjectContainer.Instance.RunQuery(new GetApplicationTypesQuery());
            var types = appTypes.Select(x => new SelectListItem() { Text = x.Item2, Value = x.Item1.ToString() });
            model.Types = types;
            return View("New", model);
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
                ScreensPathes = res.Screenshots.Select(s => string.Format("/Restricted/Screenshots/{0}{1}", s.Item1, s.Item2)),
                Package = res.ApplicationDetails.PackageFileName != null ? new ApplicationEditModel.PackageModel { Url = string.Format("/application/{0}/package", id), FileName = res.ApplicationDetails.PackageFileName } : null
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationModel model)
        {
            var packageFile = Request.Files["package_file"];
            if (packageFile.ContentLength > 0)
            {
                var ext = Path.GetExtension(packageFile.FileName);
                if (!packageExtentions.Contains(ext))
                {
                    ModelState.AddModelError("Package", "The package file is wrong format.");
                }
                else if (packageFile.ContentLength > 52428800/*50MB*/)
                {
                    ModelState.AddModelError("Package", "The package file is too big, the maximum package size is 50MB.");
                }
            }

            if (ModelState.IsValid)
            {
                var result = ObjectContainer.Instance.Dispatch(new UpdateApplicationCommand(model.Id, model.Name, model.Description, model.Type));

                if (result.Validation.Any())
                {
                    log.WriteError("Error to add application to database", string.Join("; ", result.Validation.Select(v => string.Format("Code:{0}, Message: {1}", v.ErrorCode, v.Message)).ToArray()));
                    return Redirect("/Error");
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

                    if (packageFile.ContentLength > 0)
                    {
                        // TODO: merge the command to one with create application
                        ObjectContainer.Instance.Dispatch(new UpdatePackageCommand(result.Result, packageFile.FileName));
                        var path = Path.Combine(Server.MapPath("~/Restricted/UserPackages/"), result.Result.ToString());
                        packageFile.SaveAs(path);
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
            var res = ObjectContainer.Instance.Dispatch(new RemoveApplicationCommand(id));
            if (res.Validation.Any())
            {
                log.WriteError("Error to remove application: {0} with user id: {1}", id, ObjectContainer.Instance.CurrentUserDetails.Id);
                return Redirect("/Error");
            }
            else
            {
                // remove all screens, screenshots, package and icon file
                var path = Path.Combine(Server.MapPath("~/Restricted/UserPackages/"), res.Result.AppId.ToString());
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                path = path = Path.Combine(Server.MapPath("~/Restricted/Icons/"), res.Result.AppId + res.Result.IconExt);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                foreach (var item in res.Result.Screens)
                {
                    path = path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), item.Item1 + item.Item2);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                foreach (var item in res.Result.Screenshots)
                {
                    path = path = Path.Combine(Server.MapPath("~/Restricted/Screenshots/"), item.Item1 + item.Item2);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }
            return Redirect("/Application");
        }

        public ActionResult Publish(int id)
        {
            var res = ObjectContainer.Instance.RunQuery(new TaskQuery(id));
            var countries = res.Countries.Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }).ToList();
            countries.Insert(0, new SelectListItem { Text = "All", Value = "" });
            var genders = new SelectListItem[] { new SelectListItem { Value = "", Text = "All" }, new SelectListItem { Value = "1", Text = "Men" }, new SelectListItem { Value = "2", Text = "Women" } };
            var ageRanges = GetList<AgeRange>().Select(x => new SelectListItem { Value = ((int)x).ToString(), Text = x.ToString() }).ToList();
            ageRanges.Insert(0, new SelectListItem { Text = "All", Value = "" });
            var model = new TaskModel
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
        public ActionResult Publish(TaskModel model)
        {
            if (!string.IsNullOrEmpty(model.Zip) && !model.Country.HasValue)
            {
                ModelState.AddModelError("Country", "Please select a country when zip entered.");
            }

            if (ModelState.IsValid)
            {
               // ObjectContainer.Instance.Dispatch(new AddTaskCommand(model.ApplicationId, model.AgeRange, model.Gender, model.Country, model.Zip));
                return RedirectToAction("", "Application");
            }
            else
            {
                var res = ObjectContainer.Instance.RunQuery(new TaskQuery(model.ApplicationId));
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

        private IEnumerable<T> GetList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
