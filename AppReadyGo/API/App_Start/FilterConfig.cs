using System.Web;
using System.Web.Mvc;
using AppReadyGo.API.Models.Market;

namespace AppReadyGo.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}