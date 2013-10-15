using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AppReadyGo.API.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using AppReadyGo.API.Models.Analytics;
using AppReadyGo.API.Controllers;
using AppReadyGo.Common;
using AppReadyGo.API.Models.Market;
using Common.Tests;
using AppReadyGo.Common.Tests;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.API.Tests.Controllers
{
    [TestClass]
    public class MarketClientTest
    {
#if QA
        static readonly Uri _baseAddress = new Uri("http://api.qa.appreadygo.com/market/");
#elif DEBUG
        static readonly Uri _baseAddress = new Uri("http://localhost:16989/market/");
#else
        static readonly Uri _baseAddress = new Uri("http://api.appreadygo.com/market/");
#endif


        [TestMethod]
        public void MarketGetSettingsByNetwork()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = client.GetAsync("getsettings").Result;
            if (!response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync();
                Assert.Fail(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }
            else
            {
                var res = response.Content.ReadAsStringAsync();
                //Assert.IsTrue(res.Result);
            }
        }

        [TestMethod]
        public void MarketLoginByNetwork()
        {
            var data = new UserModel { ContryId = 1, Email = "ypanshin+testapi" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com", FirstName = "xxx", Password = "121" };
            MarketByNetwork.Register(data);
            APIByNetwork.Activate(data.Email);
            MarketByNetwork.LogOn(data.Email, data.Password);
        }

        [TestMethod]
        public void MarketRegisterByNetwork()
        {
            MarketByNetwork.Register();
        }

        [TestMethod]
        public void MarketRegister()
        {
            var data = new UserModel { ContryId = 1, Email = "ypanshin+testapi" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com", FirstName = "xxx", Password = "121" };
            var controller = new MarketController();
            var model = controller.Register(data);
            Assert.IsTrue(model.Id.HasValue);

        }


        [TestMethod]
        public void MarketThirdPartyRegisterByNetwork()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new ThirdPartyUserModel { ContryId = 1, Email = "ypanshin+testapi" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com", FirstName = "xxx" };

            var task = client.PostAsJsonAsync("thirdpartyregister", data);
            var response = task.Result;
            if (!response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                Assert.Fail(string.Format("{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, res));
            }
            else
            {
                var res = response.Content.ReadAsAsync<RegisterResultModel>().Result;
                Assert.IsTrue(res.Id.HasValue);
            }
        }

        [TestMethod]
        public void MarketGetAppsByNetwork()
        {
            string userName = "ypanshin+web" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string apiUserName = "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string password = "111111";

            // New web user
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
                int appId_2 = WebByNetwork.ApplicationNew(client, "App 2", "Some description", 1, @"\..\..\..\Web.Tests\Resources\");

                // New api user
                var data = new UserModel { ContryId = 4, Gender = Gender.Women, AgeRange = AgeRange.Range12_17, Zip = "NW42RX", Email = apiUserName, FirstName = "xxx", Password = password };
                MarketByNetwork.Register(data);
                APIByNetwork.Activate(data.Email);
                var apiUserId = MarketByNetwork.LogOn(data.Email, data.Password);

                // Publish not relevant to api user app
                WebByNetwork.ApplicationPublish(client, appId_1, AgeRange.Range45_54, Gender.Men, 4, "NW42RX");

                // Get user apps (no apps)
                var appRes = MarketByNetwork.GetApps(apiUserId.Value);
                Assert.AreEqual(0, appRes.ItemsCount);

                // Publish relevant to api user app
                WebByNetwork.ApplicationPublish(client, appId_2, AgeRange.Range12_17, Gender.Women, 4, "NW42RX");

                // Get user apps (1 app)
                appRes = MarketByNetwork.GetApps(apiUserId.Value);
                Assert.AreEqual(1, appRes.ItemsCount);

                // Publish relevant to api user app
                WebByNetwork.ApplicationPublish(client, appId_2, AgeRange.Range12_17, Gender.Women, 4, "NW42RX");

                // Get user apps (1 app)
                appRes = MarketByNetwork.GetApps(apiUserId.Value);
                Assert.AreEqual(1, appRes.ItemsCount);

                // Publish relevant to api user app
                WebByNetwork.ApplicationPublish(client, appId_1, AgeRange.None, Gender.None, 4, "NW42RX");

                // Get user apps (1 app)
                appRes = MarketByNetwork.GetApps(apiUserId.Value);
                Assert.AreEqual(2, appRes.ItemsCount);
            }

        }

        [TestMethod]
        public void MarketGetAppByNetwork()
        {
            string userName = "ypanshin+web" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string apiUserName = "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string password = "111111";

            // New web user
            WebByNetwork.Register(userName, password);
            WebByNetwork.Activate(userName);
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = Global.BaseAddress })
            {
                client.Timeout = new TimeSpan(0, 3, 0);
                WebByNetwork.LogOn(client, userName, password);
                // Add two applications
                int appId = WebByNetwork.ApplicationNew(client, "App 1", "Some description", 1, @"\..\..\..\Web.Tests\Resources\");

                // New api user
                var data = new UserModel { ContryId = 4, Gender = Gender.Women, AgeRange = AgeRange.Range12_17, Zip = "NW42RX", Email = apiUserName, FirstName = "xxx", Password = password };
                MarketByNetwork.Register(data);
                APIByNetwork.Activate(data.Email);
                var apiUserId = MarketByNetwork.LogOn(data.Email, data.Password);

                WebByNetwork.ApplicationPublish(client, appId, AgeRange.Range12_17, Gender.Women, 4, "NW42RX");

                var appRes = MarketByNetwork.GetApps(apiUserId.Value);
                Assert.IsTrue(appRes.Collection.Length > 0);

                MarketByNetwork.GetApp(apiUserId.Value, appRes.Collection[0].Id);
            }
        }

        [TestMethod]
        public void MarketGetApps()
        {
            var data = new UserModel { ContryId = 1, Email = "ypanshin+testapi" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com", FirstName = "xxx", Password = "121" };
            var controller = new MarketController();
            var model = controller.Register(data);
            var apps = controller.GetApps(model.Id.Value, 1, 10);
            Assert.IsTrue(apps.ItemsCount > 0);
        }
    }
}
