using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.QueryResults.Content;
using System.Collections.Generic;
using AppReadyGo.Core;
using RazorEngine;
using System.IO;
using System.Web;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Reflection;

namespace AppReadyGo.Web.Common.Mails
{
    public abstract class Email
    {
        public string BaseTemplatePath { get; private set; }
        public string EmailPagePath { get; private set; }
        public object Model { get; protected set; }
        public IEnumerable<string> To { get; protected set; }
        public IEnumerable<string> Cc { get; protected set; }
        public IEnumerable<string> Bcc { get; protected set; }
        public string Subject { get; protected set; }

        protected Email(string baseTemplatePath, string emailPagePath)
        {
            this.BaseTemplatePath = baseTemplatePath;
            this.EmailPagePath = emailPagePath;
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
            InitRazor();

            string body = Razor.Resolve(this.EmailPagePath, this.Model).Run(new ExecuteContext());

            Messenger.SendEmail(this.To, this.Subject, body, this.Cc, this.Bcc);
        }

        public void InitRazor()
        {
            TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration();
            templateConfig.Resolver = new DelegateTemplateResolver(name =>
            {
                var absolutePath = GetAbsolutePath(name);
                return File.ReadAllText(absolutePath);
            });
            Razor.SetTemplateService(new TemplateService(templateConfig));
        }

        private string GetAbsolutePath(string path)
        {
            if (string.IsNullOrEmpty(this.BaseTemplatePath))
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            else
            {
                return this.BaseTemplatePath + path.Remove(0, 2).Replace("/", "\\");
            }
        }
    }
}