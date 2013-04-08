using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using AppReadyGo.Controllers;

namespace AppReadyGo.Common
{
    public class CSSAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            if (!(filterContext.Controller is HomeController && filterContext.ActionDescriptor.ActionName == "css"))
            {
                response.Filter = new CSSFilter(response.Filter);
            }
        }
    }
}