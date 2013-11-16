using AppReadyGo.Core;
using AppReadyGo.Web.Common.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Common.Mails
{
    public class PublishEmail:SystemEmail
    {
        private AppReadyGo.Core.QueryResults.Tasks.TaskDetailsResult task;

        public PublishEmail(int applicationId, string siteRootUrl)
        {
            Init(siteRootUrl);
        }

        public PublishEmail(AppReadyGo.Core.QueryResults.Tasks.TaskDetailsResult task)
        {
            // TODO: Complete member initialization
            this.task = task;
            Init(string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority));
        }

        private void Init(string siteRootUrl)
        {
            this.To = new PublishEmailRecipients().recipients;
          
            var mailContent = GetMailContent();

            String informationString = this.task.ApplicationName + ", Task Name:" + task.Description;
            string body = mailContent.Body.Replace("{application_name}", informationString); ;

           
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
