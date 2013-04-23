using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppReadyGo.API.Models;

namespace AppReadyGo.API.Controllers
{
    public class ReadyGoMarketController : ApiController
    {

        static readonly IMarketAppsRepository mAppsRepository = new MarketAppsRepository();
        static readonly IMarketTestersRepository mTestersRepository = new MarketTestersRepository();

        public bool GetLogin(string id, string pass)
        {
            return false;
        }

        
        public bool GetRegister(string name, string email, string pass, string gender, string contry, string zip, int[] interest, bool terms)
        {
            return true;
        }


        
        public IEnumerable<Application> GetApps(string id)
        {
            return null;
        }



    }
}
