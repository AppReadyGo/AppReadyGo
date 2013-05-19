using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Web.Common.Mails;

namespace AppReadyGo.API.Common.Mails
{
    public class APIActivationEmail : ActivationEmail
    {
       public APIActivationEmail(string email)
            : base(email, "/Activate/")
        {
        }

       public APIActivationEmail(string email, string siteRootUrl, string templateRootPath)
           : base(email, "/Activate/", siteRootUrl, templateRootPath)
        {
        }
    }
}