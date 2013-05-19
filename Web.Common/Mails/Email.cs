using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.QueryResults.Content;
using System.Collections.Generic;
using AppReadyGo.Core;
using RazorEngine;
using System.IO;
using System.Web;

namespace AppReadyGo.Web.Common.Mails
{
    public abstract class Email
    {
        public string EmailPagePath { get; private set; }
        public object Model { get; protected set; }
        public IEnumerable<string> To { get; protected set; }
        public IEnumerable<string> Cc { get; protected set; }
        public IEnumerable<string> Bcc { get; protected set; }
        public string Subject { get; protected set; }

        protected Email(string baseTemplatePath, string emailPage)
        {
            this.EmailPagePath = baseTemplatePath + emailPage;
        }

        protected Email(string baseTemplatePath, string emailPage, object model, string subject, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
            : this(baseTemplatePath, emailPage)
        {
            this.Model = model;
            this.Subject = subject;
            this.To = to;
            this.Cc = cc;
            this.Bcc = bcc;
        }

        public void Send()
        {
            var absolutePath = HttpContext.Current != null && HttpContext.Current.Server != null ? HttpContext.Current.Server.MapPath(this.EmailPagePath) : this.EmailPagePath;
            var template = File.ReadAllText(absolutePath);
            var body = Razor.Parse(template, this.Model);

            Messenger.SendEmail(this.To, this.Subject, body, this.Cc, this.Bcc);
        }
    }
}