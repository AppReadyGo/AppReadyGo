using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Common;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Model.Pages.Application;

namespace AppReadyGo.Web.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult New()
        {
            var res = ObjectContainer.Instance.RunQuery(new GetAllApplicationsQuery());
            return View("~/Views/Task/Edit.cshtml", res);
        }

    }
}
