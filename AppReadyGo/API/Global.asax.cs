using AppReadyGo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppReadyGo.API.Models.Market;
using AppReadyGo.Core.Logger;
using System.Reflection;

namespace AppReadyGo.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ObjectContainer.Instance.GetType();
        }

        protected void Application_Error(object sender, System.EventArgs e)
        {
            var context = HttpContext.Current;
            Exception ex = context.Server.GetLastError();
            // context.Server.ClearError();

            log.WriteError(ex, "Global exception");
        }
    }
}