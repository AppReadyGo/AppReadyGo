using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppReadyGo.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            //    "AjaxData",
            //    "Data/{action}/",
            //    new { controller = "Data" },
            //    new[] { "AppReadyGo.Controllers" } // Namespaces
            //);

            routes.MapRoute(
                "Home",
                "",
                new { controller = "Home", action = "Index" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Admin",
                "Admin/{action}",
                new { action = "Index", controller = "Admin" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
               "Admin_edit",
               "Admin/{action}/{id}",
               new { action = "UserEdit", controller = "Admin" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "AccountChangePassword",
                "Account/ChangePassword",
                new { controller = "Secure", action = "ChangePassword" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Account",
                "Account/{action}",
                new { controller = "Account", action = "LogOn" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Activation",
                "Account/{action}/{key}",
                new { controller = "Account", action = "Activate" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Task",
                "Task/{action}/{id}",
                new { controller = "Task", action = "New", id = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            // Appication screens
            // Example: /Application/3/screen/edit/1/some.png
            routes.MapRoute(
                "Screen controller",
                "Application/{appId}/screen/{action}/{id}/{file}",
                new { controller = "Screen", action = "Index", id = UrlParameter.Optional, file = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );  
            
            // Application controler
            // Example: /Application/edit/3
            routes.MapRoute(
                "Application controller",
                "Application/{action}/{id}",
                new { controller = "Application", action = "Index", id = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );  
            
            // Appication resources
            // Example: /Application/3/resource/icon
            routes.MapRoute(
                "Application Resources",
                "Application/{appId}/resource/{action}/{id}",
                new { controller = "ApplicationResources", action = "Icon", id = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );


            //routes.MapRoute(
            //    "ApplicationScreen",
            //    "Application/Screen/{appId}/{width}/{height}/{file}",
            //    new { controller = "Application", action = "Screen", appId = UrlParameter.Optional },
            //    new[] { "AppReadyGo.Controllers" } // Namespaces
            //);

            routes.MapRoute(
                "ApplicationClickHeatMapImage",
                "Application/ClickHeatMapImage/{appId}/{pageUri}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}/{preview}",
                new { controller = "Application", action = "ClickHeatMapImage" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "ApplicationViewHeatMapImage",
                "Application/ViewHeatMapImage/{appId}/{pageUri}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}/{preview}",
                new { controller = "Application", action = "ViewHeatMapImage" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            //routes.MapRoute(
            //    "Application",
            //    "Application/{action}/{id}/{width}/{height}/{path}/{ret}",
            //    new { controller = "Application", action = "Index", width = UrlParameter.Optional, height = UrlParameter.Optional, path = UrlParameter.Optional, ret = UrlParameter.Optional },
            //    new[] { "AppReadyGo.Controllers" } // Namespaces
            //);

            routes.MapRoute(
                "Application",
                "Application/{action}/{id}",
                new { controller = "Application", action = "Index", id = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            //routes.MapRoute(
            //    "Portfolio",
            //    "Portfolio/{action}/{id}",
            //    new { controller = "Portfolio", action = "Index", id = UrlParameter.Optional },
            //    new[] { "AppReadyGo.Controllers" } // Namespaces
            //);

            //routes.MapRoute(
            //    "JavaScript",
            //    "Analytics/{filename}.js",
            //    new { controller = "Files", action = "Analytics" }
            //);

            routes.MapRoute(
                "AnalyticsData",
                "Analytics/{action}/{portfolioId}/{applicationId}/{fromDate}/{toDate}/{screenSize}/{path}/{language}/{os}/{location}",
                new { controller = "Analytics", action = "Dashboard", applicationId = UrlParameter.Optional, fromDate = UrlParameter.Optional, toDate = UrlParameter.Optional, screenSize = UrlParameter.Optional, path = UrlParameter.Optional, language = UrlParameter.Optional, os = UrlParameter.Optional, location = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Analytics",
                "Analytics/{action}/",
                new { controller = "Analytics", action = "Index" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Secure",
                "Secure/{action}",
                new { controller = "Secure", action = "ChangePassword" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "HomeRoute",
                "Home/{action}",
                new { controller = "Home", action = "Index" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Thumbnails",
                "Thumbnails/{filename}",
                new { controller = "Files", action = "Thumbnails" },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Packages",
                "Packages/{filename}",
                new { controller = "Files", action = "Packages" }
            );

            routes.MapRoute(
                "Screens",
                "Screens/{filename}",
                new { controller = "Files", action = "Screens" }
            );

            routes.MapRoute(
                "Properties",
                "Properties/{type}/{pId}/{appId}/{filename}",
                new { controller = "Files", action = "Properties" }
            );


            //-----  Content routes
            routes.MapRoute(
               "404",
               "404",
               new { controller = "Content", action = "ErrorPage404" },
               new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
               "Error",
               "Error",
               new { controller = "Content", action = "ErrorPage" },
               new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            /*
            routes.MapRoute(
                "CSS", // Route name
                "content/css/{path1}/{path2}/{path3}/{path4}/{path5}/{path6}/{path7}", // URL with parameters
                new { controller = "Content", action = "css", path2 = UrlParameter.Optional, path3 = UrlParameter.Optional, path4 = UrlParameter.Optional, path5 = UrlParameter.Optional, path6 = UrlParameter.Optional, path7 = UrlParameter.Optional }, // Parameter defaults
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );
            */
            routes.MapRoute(
                "CSS", // Route name
                "content/cache/{version}/{file}", // URL with parameters
                new { controller = "Content", action = "Cache" }, // Parameter defaults
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Mails",
                "m/{urlPart1}/{urlPart2}/{urlPart3}/{isMail}",
                new { controller = "Content", action = "MailContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional, isMail = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );

            routes.MapRoute(
                "Pages",
                "p/{urlPart1}/{urlPart2}/{urlPart3}/",
                new { controller = "Content", action = "PageContent", urlPart2 = UrlParameter.Optional, urlPart3 = UrlParameter.Optional },
                new[] { "AppReadyGo.Controllers" } // Namespaces
            );


            //routes.MapRoute(
            //    "AjaxVisit", 
            //    "Analytics/{action}/{json}", 
            //    new { controller = "Analytics" } 
            //);/*
            //routes.MapRoute(
            //    "AjaxPackage", 
            //    "Analytics/Package/{json}", 
            //    new { controller = "Analytics", action = "Package" } 
            //);*/
            //routes.MapRoute(
            //    "ClickHeatMapImage", 
            //    "Analytics/{action}/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", 
            //    new { controller = "Analytics" } 
            //);/*
            //routes.MapRoute(
            //    "ViewHeatMapImage", 
            //    "Analytics/ViewHeatMapImage/{appId}/{pageUri}/{screenWidth}/{screenHeight}/{clientWidth}/{clientHeight}/{fromDate}/{toDate}", 
            //    new { controller = "Analytics", action = "ViewHeatMapImage" } 
            //);*/

            //routes.MapRoute(
            //    "Default", 
            //    "{controller}/{action}/{id}", 
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } 
            //);
        }
    }
}