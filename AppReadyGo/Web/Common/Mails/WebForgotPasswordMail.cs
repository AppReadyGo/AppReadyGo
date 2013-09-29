using System;
using System.Web;
using AppReadyGo.Core;
using AppReadyGo.Model.Mails;
using AppReadyGo.Web.Common;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.Common.Mails
{
    public class WebForgotPasswordMail : ForgotPasswordMail
    {
        public WebForgotPasswordMail(string email)
            : base(email, "/Account/ResetPassword/")
        {
        }
    }
}