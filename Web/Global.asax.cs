using AppReadyGo.Common;
using AppReadyGo.Core.Logger;
using AppReadyGo.CustomModelBinders;
using AppReadyGo.Model.Pages.Analytics;
using Castle.Windsor;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppReadyGo.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {

            WindsorContainer applicationWideWindsorContainer = new WindsorContainer();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();


            ModelBinders.Binders[typeof(ABFilterParametersModel)] = new ABFilterParametersModelBinder();
            ModelBinders.Binders[typeof(FilterParametersModel)] = new FilterParametersModelBinder();

            ObjectContainer.Instance.GetType();
            //ControllerBuilder.Current.SetControllerFactory(new WindsorFactory(applicationWideWindsorContainer));
            //// Initialize / install components in container
            //applicationWideWindsorContainer.Install(new WindsorInstaller());
       }
    }
}