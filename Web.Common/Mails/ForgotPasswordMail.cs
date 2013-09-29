using System;
using System.Web;
using AppReadyGo.Core;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.Common.Mails
{
    public abstract class ForgotPasswordMail : SystemEmail
    {
        public static readonly string DateFormat = "yyyy-MM-dd";

        public ForgotPasswordMail(string email, string activationPageUrl)
        {
            Init(email, activationPageUrl, string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority));
        }

        public ForgotPasswordMail(string email, string activationPageUrl, string siteRootUrl)
        {
            Init(email, activationPageUrl, siteRootUrl);
        }

        public void Init(string email, string activationPageUrl, string siteRootUrl)
        {
            this.To = new string[] { email };
            var mailContent = GetMailContent();

            string activationKey = string.Format("{0},{1}", DateTime.Now.AddDays(EmailSettings.Settings.LinksExpire.ForgotPassword).ToString(ForgotPasswordMail.DateFormat), email).EncryptLow();
            string activationLnk = string.Format("{0}{1}?key={1}", siteRootUrl, activationPageUrl, HttpUtility.UrlEncode(activationKey));
            string body = mailContent.Body.Replace("{reset_password_link}", activationLnk);

            this.Model = new SystemEmailModel(true)
            {
                ContactUsEmail = EmailSettings.Settings.Email.ContactUsEmail,
                SiteRootUrl = siteRootUrl,
                Subject = mailContent.Subject,
                Body = body
            };

            this.Subject = mailContent.Subject;
        }
    }
}