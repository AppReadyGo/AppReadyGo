using AppReadyGo.Common;
using AppReadyGo.Controllers.Master;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Model.Master;
using AppReadyGo.Model.Pages.Home;
using AppReadyGo.Models.Account;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AppReadyGo.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        public AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return AfterLoginMasterModel.MenuItem.None; }
        }

        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(ObjectContainer.Instance.CurrentUserDetails.Email, UserType.Member, UserType.Staff));
                if (securedDetails.Password == Encryption.SaltedHash(model.OldPassword, securedDetails.PasswordSalt))
                {
                    var result = ObjectContainer.Instance.Dispatch(new ResetPasswordCommand(securedDetails.Email, model.NewPassword));
                    if (result.Validation.Any())
                    {
                        //Redirect to error page
                    }
                    //return Redirect("~/p/secure/password-changed-successful");
                    return Redirect("~/");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect.");
                }
            }

            return View(model);
        }

        /// <summary>
        /// After login page content
        /// </summary>
        /// <param name="urlPart1"></param>
        /// <param name="urlPart2"></param>
        /// <param name="urlPart3"></param>
        /// <returns></returns>
        public ActionResult PageContent(string urlPart1, string urlPart2, string urlPart3)
        {
            string path = urlPart1;
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
                return null;// View("404", new PricingModel { }, BeforeLoginMasterModel.MenuItem.None);
            }
            else
            {
                var selectedItem = BeforeLoginMasterModel.MenuItem.None;
                if (!Enum.TryParse<BeforeLoginMasterModel.MenuItem>(urlPart1, true, out selectedItem))
                {
                    selectedItem = BeforeLoginMasterModel.MenuItem.None;
                }
                return View(new ContentModel(selectedItem) { Title = page.Title, Content = page.Content });
            }
        }
    }
}
