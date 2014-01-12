using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Common;
using AppReadyGo.Core.Commands.Applications;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Model;
using AppReadyGo.Model.Pages.Application;

namespace AppReadyGo.Web.Controllers
{
    public class ScreenController : Controller
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public FileContentResult Image(int appId, int id)
        {
            var screen = ObjectContainer.Instance.RunQuery(new GetScreenDetailsQuery(id));

            var dir = Server.MapPath(string.Format("~/Restricted/Screens/{0}{1}", screen.Id, screen.FileExtension));
            if (System.IO.File.Exists(dir))
            {
                return new FileContentResult(System.IO.File.ReadAllBytes(dir), "image/" + screen.FileExtension.Replace(".", "")); ;
            }
            else
            {
                throw new HttpException(404, "Image Not found");
            }
        }

        public ActionResult Index(int appId, string srch = "", int scol = 1, int cp = 1, string orderby = "", string order = "")
        {
            var orderBy = string.IsNullOrEmpty(orderby) ? ScreensQuery.OrderByColumn.Path : (ScreensQuery.OrderByColumn)Enum.Parse(typeof(ScreensQuery.OrderByColumn), orderby, true);
            bool asc = string.IsNullOrEmpty(orderby) ? ((orderBy == ScreensQuery.OrderByColumn.Path) ? false : true) : order.Equals("asc", StringComparison.OrdinalIgnoreCase);
            var data = ObjectContainer.Instance.RunQuery(new ScreensQuery(appId, srch, cp, 10, orderBy, asc));
            var searchStrUrlPart = string.IsNullOrEmpty(srch) ? string.Empty : string.Concat("&srch=", HttpUtility.UrlEncode(srch));

            var invalidChars = System.IO.Path.GetInvalidFileNameChars();

            var model = new ScreensListModel
            {
                Pagging = new PagingModel
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
                ApplicationName = data.ApplicationName,
                Screens = data.Screens.Select((s, i) => new ScreenItemModel
                {
                    Id = s.Id,
                    Width = s.Width,
                    Height = s.Height,
                    Path = s.Path,
                    FileName = new string(s.Path.Where(m => !invalidChars.Contains(m)).ToArray<char>()) + s.FileExtension,
                    IsAlternative = i % 2 != 0,
                }).ToArray()
            };
            return View(model);
        }

        public ActionResult New(int appId)
        {
            return GetActionData(appId, new ScreenModel { ApplicationId = appId });
        }


        [HttpPost]
        public ActionResult New(ScreenModel model)
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
            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
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
                    return Redirect("/Error");
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), result.Result + fileExtention);
                    log.WriteInformation("Save file: {0}", path);
                    Request.Files[0].SaveAs(path);
                }

                return RedirectToAction("", new { appId = model.ApplicationId });

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

        public ActionResult Edit(int appId, int id/*, ScreenReturn ret*/)
        {
            var data = ObjectContainer.Instance.RunQuery(new GetScreenEditDataQuery(id));
            if (data == null)
            {
                return Redirect("~/Error");
            }
            else
            {
                var invalidChars = System.IO.Path.GetInvalidFileNameChars();
                var model = new EditScreenModel
                {
                    Id = data.Id,
                    Path = data.Path,
                    Width = data.Width,
                    Height = data.Height,
                    FileExtention = data.FileExtention,
                    FileName = new string(data.Path.Where(m => !invalidChars.Contains(m)).ToArray<char>()) + data.FileExtention,
                    ApplicationId = data.ApplicationId
                };
                return PrepareAction(model, data);
            }
        }

        [HttpPost]
        public ActionResult Edit(ScreenModel model)
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
                    return Redirect("~/Error");
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
                return RedirectToAction("", new { appId = model.ApplicationId });
            }
            else
            {
                return GetActionData(model.ApplicationId, model);
            }
        }

        public ActionResult Remove(int appId, int id)
        {
            var screen = ObjectContainer.Instance.RunQuery(new GetScreenDetailsQuery(id));
            if (screen == null)
            {
                return Redirect("~/Error");
            }
            else
            {
                ObjectContainer.Instance.Dispatch(new RemoveScreenCommand(id));
                var path = Path.Combine(Server.MapPath("~/Restricted/Screens/"), screen.Id + screen.FileExtension);
                System.IO.File.Delete(path);
            }
            return RedirectToAction("", new { appId = screen.ApplicationId });
        }

        private ActionResult GetActionData(int id, ScreenModel model)
        {
            var data = ObjectContainer.Instance.RunQuery(new GetScreenDataQuery(id));

            return PrepareAction(model, data);
        }

        private ActionResult PrepareAction(ScreenModel model, ScreenDataResult data)
        {
            ViewBag.ApplicationDescription = data.ApplicationName;

            var pathes = data.Pathes.Select(p => new SelectListItem { Text = p, Value = p }).ToList();
            pathes.Insert(0, new SelectListItem { Text = "Select from exists", Value = "" });
            ViewData["predefinedPathes"] = pathes;

            var sizes = data.Sizes.Select(s => new SelectListItem { Text = s.Width + "X" + s.Height, Value = s.Width + "X" + s.Height }).ToList();
            sizes.Insert(0, new SelectListItem { Text = "Select from exists", Value = "X" });
            ViewData["predefinedSizes"] = sizes;

            return View(model);
        }
    }
}
