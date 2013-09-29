using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Common.Mails;

namespace AppReadyGo.API.Common.Mails
{
    public class APIForgotPasswordMail : ForgotPasswordMail
    {
       public APIForgotPasswordMail(string email)
            : base(email, "/ResetPassword/")
        {
        }

       public APIForgotPasswordMail(string email, string siteRootUrl)
           : base(email, "/ResetPassword/", siteRootUrl)
        {
        }
    }
}