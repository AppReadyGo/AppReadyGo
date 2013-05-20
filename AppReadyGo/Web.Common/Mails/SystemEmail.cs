using AppReadyGo.Common;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.QueryResults.Content;
using System.Collections.Generic;

namespace AppReadyGo.Web.Common.Mails
{
    public abstract class SystemEmail : Email
    {
        public SystemEmail(string templateRootPath)
            : base(templateRootPath, "~/Views/Mails/System.cshtml")
        {
        }

        public SystemEmail()
            : this(null)
        {
        }

        protected MailResult GetMailContent()
        {
            return ObjectContainer.Instance.RunQuery(new GetSystemMailQuery(string.Format("mails/{0}", this.GetType().Name).ToLower()));
        }
    }
}