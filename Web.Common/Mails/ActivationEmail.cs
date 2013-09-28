using System;
using System.Web;
using AppReadyGo.Core;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.Web.Common.Mails
{
    public abstract class ActivationEmail : SystemEmail
    {
        public static readonly string DateFormat = "yyyy-MM-dd";

        public ActivationEmail(string email, string activationPageUrl)
        {
            Init(email, activationPageUrl, string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority));
        }

        public ActivationEmail(string email, string activationPageUrl, string siteRootUrl)
        {
            Init(email, activationPageUrl, siteRootUrl);
        }

        private void Init(string email, string activationPageUrl, string siteRootUrl)
        {
            this.To = new string[] { email };
            var mailContent = GetMailContent();

            string activationKey = string.Format("{0},{1}", DateTime.Now.AddDays(EmailSettings.Settings.LinksExpire.Activation).ToString(DateFormat), email).EncryptLow();
            string activationLnk = string.Format("{0}{1}?key={2}", siteRootUrl, activationPageUrl, HttpUtility.UrlEncode(activationKey));
            string body = mailContent.Body.Replace("{activation_link}", activationLnk);

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