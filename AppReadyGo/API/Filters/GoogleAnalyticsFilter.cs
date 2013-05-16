using AppReadyGo.Core.Logger;
using GoogleAnalyticsDotNet.Common;
using GoogleAnalyticsDotNet.Common.Data;
using GoogleAnalyticsDotNet.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.API.Filters
{
    public class GoogleAnalyticsFilter : ActionFilterAttribute, IActionFilter
    {
        private static bool googleAnalytics = bool.Parse(ConfigurationManager.AppSettings["GoogleAnalytics"]);
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (googleAnalytics)
            {
                // Google analytics
                GooglePageView pageView = new GooglePageView("Submit Package", "api.appreadygo.com", string.Format("/{0}/{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName));
                TrackingRequest request = new RequestFactory().BuildRequest(pageView);
                GoogleTracking.FireTrackingEvent(request);
            }
            log.WriteInformation("Google analytics throw action filter");
            this.OnActionExecuting(filterContext);
        }
    }
}