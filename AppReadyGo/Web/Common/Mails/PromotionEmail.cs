using System.Web;
using AppReadyGo.Core;
using AppReadyGo.Model.Mails;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.Common.Mails
{
    public class PromotionEmail : Email
    {
        public PromotionEmail(string emailKey, string email, bool isEmailProcess = true)
            : this(emailKey, ObjectContainer.Instance.RunQuery(new GetUserDetailsByEmailQuery(email)), isEmailProcess)
        {
        }

        public PromotionEmail(string emailKey, UserDetailsResult userDetails, bool isEmailProcess = true)
            : base("~/Views/Mails/Promotion.cshtml")
        {
            this.To = new string[] { userDetails.Email };
            string contentPath = string.Format("mails/{0}", emailKey);
            var mailContent = ObjectContainer.Instance.RunQuery(new GetMailQuery(contentPath.ToLower()));

            string siteRootUrl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
            string userName = string.IsNullOrEmpty(userDetails.FirstName) || string.IsNullOrEmpty(userDetails.LastName) ? userDetails.Email : string.Join(" ", userDetails.FirstName, userDetails.LastName);
            string body = mailContent.Body.Replace("{user_name}", userName);

            this.Model = new PromotionEmailModel(isEmailProcess)
            {
                ContactUsEmail = EmailSettings.Settings.Email.ContactUsEmail,
                SiteRootUrl = siteRootUrl,
                Subject = mailContent.Subject,
                Body = body,
                ThisEmailUrl = string.Format("{0}/mails/{1}/{2}", siteRootUrl, emailKey, userDetails.Email),
                UnsubscribeUrl = string.Format("{0}/Account/Unsubscribe/{1}", siteRootUrl, userDetails.Email)
            };

            this.Subject = mailContent.Subject;
        }
    }
}