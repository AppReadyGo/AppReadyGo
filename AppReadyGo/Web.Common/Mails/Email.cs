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
using System.Web.Mvc;
using System.Web.Routing;
using Mvc.Mailer;
using System.Configuration;

namespace AppReadyGo.Web.Common.Mails
{
    public abstract class Email
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string BodyGenerator = ConfigurationManager.AppSettings["EmailBodyGenerator"];

        public static string BaseTemplatePath { get; private set; }

        public string EmailPagePath { get; private set; }
        public object Model { get; protected set; }
        public IEnumerable<string> To { get; protected set; }
        public IEnumerable<string> Cc { get; protected set; }
        public IEnumerable<string> Bcc { get; protected set; }
        public string Subject { get; protected set; }

        public ControllerContext ControllerContext { get; protected set; }

        protected Email(string emailPagePath)
        {
            this.EmailPagePath = emailPagePath;
        }

        protected Email(string emailPage, object model, string subject, IEnumerable<string> to, IEnumerable<string> cc = null, IEnumerable<string> bcc = null)
            : this(emailPage)
        {
            this.Model = model;
            this.Subject = subject;
            this.To = to;
            this.Cc = cc;
            this.Bcc = bcc;
        }

        public void Send()
        {

            string body = string.Empty;
            if (BodyGenerator == "RazorEngine")
            {
                body = Razor.Resolve(this.EmailPagePath, this.Model).Run(new ExecuteContext());
            }
            else if (BodyGenerator == "MVCMailer")
            {
                body = GetMVCMailerBody();
            }
            else
            {
                body = RenderViewToString(this.ControllerContext, this.EmailPagePath, new ViewDataDictionary(this.Model), new TempDataDictionary());
            } 
        
            log.WriteInformation("Send email:{0}, {1}, {2}, {3}, {4}, {5}", string.Join(";", this.To), this.Subject, this.Cc == null ? "" : string.Join(";", this.Cc), this.Bcc == null ? "" : string.Join(";", this.Bcc), this.EmailPagePath, body);

            Messenger.SendEmail(this.To, this.Subject, body, this.Cc, this.Bcc);
        }

        private string GetMVCMailerBody()
        {
            var mailer = new MailerBase();
            mailer.ViewData = new ViewDataDictionary(this.Model);
            var message = mailer.Populate(x =>
            {
                x.Subject = this.Subject;
                x.ViewName = this.EmailPagePath;
                foreach (var item in this.To)
                {
                    x.To.Add(item);
                }
            });
            return message.Body;
        }

        private string RenderViewToString(ControllerContext controllerContext, string viewPath, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            ViewPage viewPage = new ViewPage();
            StringWriter strWriter = new StringWriter();

            //Right, create our view
            viewPage.ViewContext = new ViewContext(controllerContext, new RazorView(controllerContext, viewPath, "~/Views/Shared/_Mail.cshtml", false, null), viewData, tempData, strWriter);

            //Now render the view into the memorystream and flush the response
            viewPage.ViewContext.View.Render(viewPage.ViewContext, viewPage.ViewContext.HttpContext.Response.Output);

            return strWriter.ToString();
        }

        public static void InitRazor(string baseTemplatePath = null)
        {
            BaseTemplatePath = BaseTemplatePath;

            TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration();
            templateConfig.Resolver = new DelegateTemplateResolver(name =>
            {
                var absolutePath = GetAbsolutePath(name);
                log.WriteInformation("Email template absolute path:{0}, file exists:{1}", absolutePath, File.Exists(absolutePath));
                return File.ReadAllText(absolutePath);
            });
            Razor.SetTemplateService(new TemplateService(templateConfig));
        }

        private static string GetAbsolutePath(string path)
        {
            if (string.IsNullOrEmpty(BaseTemplatePath))
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            else
            {
                return BaseTemplatePath + path.Remove(0, 2).Replace("/", "\\");
            }
        }
    }
}