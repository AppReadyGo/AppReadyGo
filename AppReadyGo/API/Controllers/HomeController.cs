using AppReadyGo.Common;
using AppReadyGo.Core.Commands.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Core;

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
    }
}
