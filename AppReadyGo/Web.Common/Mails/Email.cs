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
using System;
using AppReadyGo.Core.Logger;
using System.Threading;

namespace AppReadyGo.Web.Common.Mails
{
    public abstract class Email
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        public string BaseTemplatePath { get; private set; }
        public string EmailPagePath { get; private set; }
        public object Model { get; protected set; }
        public IEnumerable<string> To { get; protected set; }
        public IEnumerable<string> Cc { get; protected set; }
        public IEnumerable<string> Bcc { get; protected set; }
        public string Subject { get; protected set; }

        private ReaderWriterLockSlim templatesLock = new ReaderWriterLockSlim();
        private static List<string> Templates = new List<string>();

        protected Email(string baseTemplatePath, string emailPagePath)
        {
            this.BaseTemplatePath = baseTemplatePath;
            this.EmailPagePath = emailPagePath;

            InitRazor();
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
            log.WriteInformation("Send email:{0}, {1}, {2}, {3}, {4}", string.Join(";", this.To), this.Subject, this.Cc == null ? "" : string.Join(";", this.Cc), this.Bcc == null ? "" : string.Join(";", this.Bcc), this.EmailPagePath);


            string body = Razor.Resolve(this.EmailPagePath, this.Model).Run(new ExecuteContext());

            Messenger.SendEmail(this.To, this.Subject, body, this.Cc, this.Bcc);
        }

        private void InitRazor()
        {
            var className = this.GetType().Name;
            templatesLock.EnterUpgradeableReadLock();
            try
            {
                if (!Templates.Contains(className))
                {
                    templatesLock.EnterWriteLock();
                    try
                    {

                        TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration();
                        templateConfig.Resolver = new DelegateTemplateResolver(name =>
                        {
                            var absolutePath = GetAbsolutePath(name);
                            log.WriteInformation("Email template absolute path:{0}, file exists:{1}", absolutePath, File.Exists(absolutePath));
                            return File.ReadAllText(absolutePath);
                        });
                        Razor.SetTemplateService(new TemplateService(templateConfig));

                        Templates.Add(className);
                    }
                    finally
                    {
                        templatesLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                templatesLock.ExitUpgradeableReadLock();
            }
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