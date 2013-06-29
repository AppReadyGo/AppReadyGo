using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AppReadyGo.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Market",
                routeTemplate: "Market/{action}/{id}",
                defaults: new { controller = "Market", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Analytics",
                routeTemplate: "Analytics/{action}/{id}",
                defaults: new { controller = "Analytics", id = RouteParameter.Optional }
            );
        }
    }
}
