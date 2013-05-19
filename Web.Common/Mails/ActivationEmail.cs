using System;
using System.Web;
using AppReadyGo.Core;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.Web.Common.Mails
{
    public class ActivationEmail : SystemEmail
    {
        public ActivationEmail(string email, string activationPageUrl)
            : this(email, activationPageUrl, string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority))
        {
        }

        public ActivationEmail(string email, string activationPageUrl, string siteRootUrl)
        {
            this.To = new string[] { email };
            var mailContent = GetMailContent();

            string activationKey = string.Format("{0},{1}", DateTime.Now.AddDays(EmailSettings.Settings.LinksExpire.Activation), email).EncryptLow();
            string activationLnk = string.Format("{0}{1}{1}", siteRootUrl, activationPageUrl, HttpUtility.UrlEncode(activationKey));
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