using AppReadyGo.Common;
using AppReadyGo.Core.Commands.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.API.Models;
using AppReadyGo.API.Common.Mails;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult(ConfigurationManager.AppSettings["WebApplicationBaseUrl"]);
        }

        public ActionResult Activate(string key)
        {
            var splitedKey = key.DecryptLow().Split(',');
            if (DateTime.Now > DateTime.Parse(splitedKey[0]))
            {
                throw new Exception("Activation link expired.");
            }
            var result = ObjectContainer.Instance.Dispatch(new ActivateUserCommand(splitedKey[1]));
            if (result.Validation.Any())
            {
                throw new Exception("User was not found.");
            }
            ObjectContainer.Instance.Dispatch(new GrantSpecialAccessCommand(result.Result.Value, true));
            return Redirect("~/p/account-activated");
        }

        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            string path = "api/" + urlPart1;
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
                throw new Exception("Page not found");
            }
            else
            {
                return View("PageContent", new ContentModel() { Title = page.Title, Content = page.Content });
            }
        }
    }
}
