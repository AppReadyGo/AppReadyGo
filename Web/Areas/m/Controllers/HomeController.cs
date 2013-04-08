using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Home;
using AppReadyGo.Common;

namespace AppReadyGo.Areas.m.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            string path = urlPart1 == "s" ? urlPart1 : "m/" + urlPart1;
            if (!string.IsNullOrEmpty(urlPart2))
            {
                path += "/" + urlPart2;
            }
            if (!string.IsNullOrEmpty(urlPart3))
            {
                path += "/" + urlPart3;
            }

            var page = ObjectContainer.Instance.RunQuery(new GetPageQuery(path.ToLower()));
            if (page == null)
            {
                return View("404");
            }
            else
            {
                BeforeLoginMasterModel.MenuItem selectedItem = BeforeLoginMasterModel.MenuItem.None;
                if (!Enum.TryParse<BeforeLoginMasterModel.MenuItem>(urlPart1, true, out selectedItem))
                {
                    selectedItem = BeforeLoginMasterModel.MenuItem.None;
                }
                return View(new ContentModel { Title = page.Title, Content = page.Content });
            }
        }
    }
}
