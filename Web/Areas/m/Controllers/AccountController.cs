using AppReadyGo.Common;
using AppReadyGo.Common.Mails;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Models.Account;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AppReadyGo.Areas.m.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return this.View(new RegisterModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = ObjectContainer.Instance.Dispatch(new CreateMemberCommand(model.Email, model.Password));

                if (!result.Validation.Any())
                {
                    new MailGenerator().Send(new WebActivationEmail(model.Email));
                    return Redirect("/m/activation-email-sent");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(result.Validation));
                }
            }

            return this.View(model);
        }

        private string ErrorCodeToString(IEnumerable<ValidationResult> validations)
        {
            if (validations.Any(v => v.ErrorCode == ErrorCode.EmailExists))
            {
                return "User with the email already exists, please check your email.";
            }
            else if (validations.Any(v => v.ErrorCode == ErrorCode.WrongEmail))
            {
                return "Email has wrong format, please correct and try again.";
            }
            return "System error, please contact to administrator.";
        }
    }
}
