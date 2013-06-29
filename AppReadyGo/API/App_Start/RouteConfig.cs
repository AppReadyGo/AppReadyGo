using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppReadyGo.API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
 
            routes.MapRoute(
                name: "Default",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Activate",
                url: "Activate/{key}",
                defaults: new { controller = "Home", action = "Activate" }
            );

            routes.MapRoute(
                "Pages",
                "p/{urlPart1}/{urlPart2}/{urlPart3}/",
                new { controller = "Home", action = "PageContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional }
            );
        }
    }
}