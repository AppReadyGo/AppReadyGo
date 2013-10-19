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
using AppReadyGo.Core.Queries.Users;

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
            var result = ObjectContainer.Instance.Dispatch(new ActivateUserCommand(splitedKey[1], UserType.ApiMember));
            if (result.Validation.Any())
            {
                throw new Exception("User was not found.");
            }
            ObjectContainer.Instance.Dispatch(new GrantSpecialAccessCommand(result.Result.Value, true));
            return Redirect("~/p/account-activated");
        }

        public ActionResult ResetPassword(string key)
        {
            var splitedKey = key.DecryptLow().Split(',');
            if (DateTime.Now > DateTime.Parse(splitedKey[0]))
            {
                throw new Exception("Reset password link expired.");
            }
            var userDetails = ObjectContainer.Instance.RunQuery(new GetUserDetailsByEmailQuery(splitedKey[1]));
            if (userDetails == null)
            {
                throw new Exception("User not found.");
            }
            return View(new ResetPasswordModel { Key = key });
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model, string key)
        {
            if (ModelState.IsValid)
            {
                var splitedKey = key.DecryptLow().Split(',');
                var email = splitedKey[1];
                if (DateTime.Now > DateTime.Parse(splitedKey[0]))
                {
                    throw new Exception("Reset password link expired.");
                }
                var result = ObjectContainer.Instance.Dispatch(new ResetPasswordCommand(email, model.NewPassword));
                if (result.Validation.Any())
                {
                    if (result.Validation.Any(x => x.ErrorCode == ErrorCode.WrongPassword))
                    {
                        ModelState.AddModelError("error", "The password your enter is too weak.");
                    }
                    else if (result.Validation.Any(x => x.ErrorCode == ErrorCode.EmailDoesNotExists))
                    {
                        throw new Exception("User not found.");
                    }
                    else
                    {
                        throw new Exception("Error to reset password:{0}" + string.Join(", ", result.Validation.Select(x => x.ErrorCode.ToString())));
                    }
                }
                else
                {
                    var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(email, UserType.ApiMember));
                    if (!securedDetails.Activated)
                    {
                        ObjectContainer.Instance.Dispatch(new ActivateUserCommand(email, UserType.ApiMember));
                    }


                    return Redirect("~/p/password-changed");
                }
            }

            return View(model);
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
