using AppReadyGo.Common;
using AppReadyGo.Common.Mails;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Model;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Account;
using AppReadyGo.Models.Account;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AppReadyGo.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            return this.View(new LogOnModel());
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(model.UserName));
                if (securedDetails == null || securedDetails.Password != Encryption.SaltedHash(model.Password, securedDetails.PasswordSalt))
                {
                    ModelState.AddModelError("error", "The user name or password provided is incorrect.");
                }
                else if(!securedDetails.Activated)
                {
                    ModelState.AddModelError("error", "You account is not activated, please use the link from activation email to activate your account.");
                }
                // Temprorary access just for special users
                else if (!securedDetails.SpecialAccess && securedDetails.Roles == null)
                {
                    return Redirect("~/p/special-access-required");
                }
                else if (!Membership.Provider.ValidateUser(model.UserName, model.Password))
                {
                    ModelState.AddModelError("error", "The user name or password provided is incorrect.");
                }
                //else if (!securedDetails.AcceptedTermsAndConditions)
                //{
                //    Session["id"] = securedDetails.Id;
                //    Session["rememberMe"] = model.RememberMe;
                //    Session["returnUrl"] = returnUrl;
                //    return RedirectToAction("TermsAndCoditions");
                //}
                else
                {
                    return LogIn(securedDetails.Id, model.RememberMe, returnUrl);
                }
            }

            return this.View(model);
        }

        public ActionResult TermsAndConditions()
        {
            var key = ObjectContainer.Instance.RunQuery(new GetKeyQuery("account/terms-and-coditions"));
            ViewBag.Content = key.Items.First().Value;
            return this.View(new  BeforeLoginMasterModel(BeforeLoginMasterModel.MenuItem.None));
        }

        [HttpPost]
        public ActionResult AcceptTermsAndCoditions()
        {
            int id = (int)Session["id"];
            bool rememberMe = (bool)Session["rememberMe"];
            string returnUrl = (string)Session["returnUrl"];

            Session["id"] = null;
            Session["rememberMe"] = null;
            Session["returnUrl"] = null;

            ObjectContainer.Instance.Dispatch(new AcceptTermsAndConditionsCommand(id));

            return this.LogIn(id, rememberMe, returnUrl);
        }

        private ActionResult LogIn(int id, bool rememberMe, string returnUrl)
        {
            FormsAuthentication.SetAuthCookie(id.ToString(), rememberMe);

            ObjectContainer.Instance.Dispatch(new UpdateLastAccessCommand(id));

            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            ObjectContainer.Instance.ClearCurrentUserDetails();

            return RedirectToAction("Index", "Home");
        }

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
                    new WebActivationEmail(model.Email).Send();
                    return Redirect("~/p/activation-email-sent");
                }
                else
                {
                    ModelState.AddModelError("error", ErrorCodeToString(result.Validation));
                }
            }

            return this.View(model);
        }

        private string ErrorCodeToString(IEnumerable<ValidationResult> validations)
        {
            if(validations.Any(v => v.ErrorCode == ErrorCode.EmailExists))
            {
                return "User with the email already exists, please check your email.";
            }
            else if (validations.Any(v => v.ErrorCode == ErrorCode.WrongEmail))
            {
                return "Email has wrong format, please correct and try again.";
            }
            return "System error, please contact to administrator.";
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
            return Redirect("~/p/account-activated"); ;
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordModel());
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var userDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(model.Email));
                if (userDetails != null)
                {
                    new ForgotPasswordMail(model.Email).Send();
                    return Redirect("~/p/forgot-password-email-sent"); // Redirect to content page
                }
                else
                {
                    ModelState.AddModelError("error", "The user does not exits in the system.");
                }
            }

            return View(model);
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
                    var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(email));
                    if (!securedDetails.Activated)
                    {
                        ObjectContainer.Instance.Dispatch(new ActivateUserCommand(email));
                    }
                    // Temprorary access just for special users
                    if (!securedDetails.SpecialAccess && securedDetails.Roles == null)
                    {
                        return Redirect("~/p/special-access-required");
                    }
                    else
                    {
                        // TODO: add terms and conditions
                        FormsAuthentication.SetAuthCookie(securedDetails.Id.ToString(), false);

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Unsubscribe(string email)
        {
            return View(new UnsubscribeModel { Email = email });
        }

        [HttpPost]
        public ActionResult Unsubscribe(UnsubscribeModel model)
        {
            if (ModelState.IsValid)
            {
                var result = ObjectContainer.Instance.Dispatch(new UnsubscribeCommand(model.Email));
                if (result.Validation.Any())
                {
                    ModelState.AddModelError("error", "Wrong email.");
                }
                return Redirect("~/p/unsubscrubed-successful");
            }
            return View(model);
        }


        public ActionResult Details()
        {
            return View();
        }
    }
}
