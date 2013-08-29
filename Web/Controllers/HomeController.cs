using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AppReadyGo.Core;
using System.Web.Security;
using AppReadyGo.Model.Pages.Home;
using AppReadyGo.Model.Master;
using AppReadyGo.Core.Queries;
using AppReadyGo.Model.Pages.Home.Mails;
using AppReadyGo.Common.Mails;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using AppReadyGo.Common;
using AppReadyGo.Core.Queries.Content;

namespace AppReadyGo.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (ObjectContainer.Instance.CurrentUserDetails.Type == UserType.Staff)
                {
                    return RedirectToAction("", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Application");
                }
            }
            else
            {
                var key = ObjectContainer.Instance.RunQuery(new GetKeyQuery("home"));
                ViewBag.Slideshow = key.Items.Single(x => x.SubKey == "slideshow").Value;
                ViewBag.Content = key.Items.Single(x => x.SubKey == "content").Value;
                return View(new BeforeLoginMasterModel(BeforeLoginMasterModel.MenuItem.Home));
            }
        }

        public ActionResult Pricing()
        {
            return View(new BeforeLoginMasterModel(BeforeLoginMasterModel.MenuItem.PlanAndPricing));
        }

        public ActionResult PlayGround()
        {
            return View(new BeforeLoginMasterModel(BeforeLoginMasterModel.MenuItem.PlanAndPricing));
        }
    }
}
