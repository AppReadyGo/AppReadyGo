using System;
using System.Web;
using AppReadyGo.Core;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.Common.Mails
{
    public class WebActivationEmail : AppReadyGo.Web.Common.Mails.ActivationEmail
    {
        public WebActivationEmail(string email)
            : base(email, "/Account/Activate/")
        {
        }
    }
}