using AppReadyGo.Core;
using AppReadyGo.Model.Mails;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;
using System.Web;

namespace AppReadyGo.Common.Mails
{
    public class SpecialAccessEmail : SystemEmail
    {
        public SpecialAccessEmail(string email)
        {
            this.To = new string[] { email };
            var mailContent = GetMailContent();

           
            string siteRootUrl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
            string body = mailContent.Body;

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