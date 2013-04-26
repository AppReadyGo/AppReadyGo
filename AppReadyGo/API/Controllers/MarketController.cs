using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppReadyGo.API.Models;

namespace AppReadyGo.API.Controllers
{
    public class MarketController : ApiController
    {
        // TODO: Yura: Why do we need the repositories? We already have DAL, this is just add additional layer that we don't need
        // In addition the approach to init objects, prevent test creating.
        static readonly IMarketAppsRepository mAppsRepository = new MarketAppsRepository();
        static readonly IMarketTestersRepository mTestersRepository = new MarketTestersRepository();

        [HttpPost]
        public bool Login(string id, string pass)
        {
            return false;
        }


        // TODO: Yura: We do not need the last parameter, you will not call for the method if user did not accept terms and conditions.
        [HttpPost]
        public bool Register(string name, string email, string pass, string gender, string contry, string zip, int[] interest, bool terms)
        {
            return true;
        }


        [HttpGet]
        public IEnumerable<Application> GetApps(string id)
        {
            return null;
        }



    }
}
