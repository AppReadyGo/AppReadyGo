using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Common;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Model.Pages.Application;

namespace AppReadyGo.Web.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult New()
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
            return View("~/Views/Task/Edit.cshtml", model);
        }

    }
}
