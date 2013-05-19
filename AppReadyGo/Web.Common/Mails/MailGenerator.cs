using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using AppReadyGo.Core;
using RazorEngine;

namespace AppReadyGo.Web.Common.Mails
{
    public class MailGenerator
    {
        //public ControllerContext ControllerContext { get; private set; }

        //public MailGenerator(ControllerContext controllerContext/*, string urlPath1, string urlPath2, string urlPath3*/)
        //{
        //    /*
        //    var view = (ViewResult)new HomeController().MailContent(urlPath1, urlPath2, urlPath3, true);
        //    this.ViewData = view.ViewData;
        //    this.TempData = view.TempData;
        //    this.Model = ((BasicMailContentModel)view.ViewData.Model);
        //    */
        //    this.ControllerContext = controllerContext;
        //}

        //private string RenderViewToString(ControllerContext controllerContext, string viewPath, ViewDataDictionary viewData, TempDataDictionary temData)
        //{
        //    ViewPage viewPage = new ViewPage();
        //    StringWriter strWriter = new StringWriter();

        //    //Right, create our view
        //    viewPage.ViewContext = new ViewContext(controllerContext, new WebFormView(controllerContext, viewPath), viewData, temData, strWriter);

        //    //Now render the view into the memorystream and flush the response
        //    viewPage.ViewContext.View.Render(viewPage.ViewContext, viewPage.ViewContext.HttpContext.Response.Output);

        //    return strWriter.ToString();
        //}

        //public void Send<TEmail>(TEmail email)
        //    where TEmail : Email
        //{
        //    string body = RenderViewToString(this.ControllerContext, email.EmailPagePath, new ViewDataDictionary(email.Model), new TempDataDictionary());

        //    Messenger.SendEmail(email.To, email.Subject, body, email.Cc, email.Bcc);
        //}

        public void Send<TEmail>(TEmail email)
                        where TEmail : Email
        {
            var template = File.ReadAllText(email.EmailPagePath);
            var body = Razor.Parse(template, email.Model);

            Messenger.SendEmail(email.To, email.Subject, body, email.Cc, email.Bcc);
        }
    }
}