using System;
using System.Net;
using System.Net.Http;
using AppReadyGo.Core.Entities;
using Common.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppReadyGo.Web.Tests.Controllers
{
    [TestClass]
    public class ApplicationControllerTest
    {
        [TestMethod]
        public void Publish()
        {
            string userName = "ypanshin+web" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string password = "111111";

            WebByNetwork.Register(userName, password);
            WebByNetwork.Activate(userName);
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = Global.BaseAddress })
            {
                client.Timeout = new TimeSpan(0, 3, 0);
                WebByNetwork.LogOn(client, userName, password);
                // Add two applications
                int appId_1 = WebByNetwork.ApplicationNew(client, "App 1", "Some description", 1, @"\..\..\..\Web.Tests\Resources\");

                // Publish not relevant to api user app
                WebByNetwork.ApplicationPublish(client, appId_1, AgeRange.Range45_54, Gender.Men, 4, "NW42RX");
            }
        }
    }
}
