using System;
using System.Linq;
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
using Common.Tests;
using AppReadyGo.API.Models.Market;
using AppReadyGo.Core.Entities;
using AppReadyGo.Common.Tests;

namespace AppReadyGo.API.Tests.Controllers
{
    [TestClass]
    public class AnalyticsClientTest
    {
        static readonly Uri _baseAddress = new Uri("http://localhost:63321/analytics/");

        [TestMethod]
        public void AnalyticsIndexByNetwork()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(string.Empty).Result;
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }
            else
            {
                var res = response.Content.ReadAsAsync<bool>();
                Assert.IsTrue(res.Result);
            }
        }

        [TestMethod]
        public void AnalyticsSubmitPackage()
        {
            ObjectContainer.Instance.GetType();
            var data = AnalyticsByNetwork.CreateRandomPackage(1, 320, 480);
            var controller = new AnalyticsController();
            var view = controller.SubmitPackage(data);
            Assert.IsTrue(view);
        }

        [TestMethod]
        public void AnalyticsSubmitPackageByNetwork()
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
                client.Timeout = new TimeSpan(0, 4, 0);
                WebByNetwork.LogOn(client, userName, password);
                // Add two applications
                int appId = WebByNetwork.ApplicationNew(client, "App 1", "Some description", 1, @"\..\..\..\Web.Tests\Resources\");

                // New api user
                var data = new UserModel { ContryId = 4, Gender = Gender.Women, AgeRange = AgeRange.Range12_17, Zip = "NW42RX", Email = apiUserName, FirstName = "xxx", Password = password };
                MarketByNetwork.Register(data);
                APIByNetwork.Activate(data.Email);
                var apiUserId = MarketByNetwork.LogOn(data.Email, data.Password);

                WebByNetwork.ApplicationPublish(client, appId, AgeRange.Range12_17, Gender.Women, 4, "NW42RX");

                var appRes = MarketByNetwork.GetApps(apiUserId.Id.Value);
                Assert.IsTrue(appRes.Collection.Length > 0);

                AnalyticsByNetwork.SubmitPackageByNetwork(appRes.Collection.Select(a => a.Id).Max(), 320, 480);
            }
        }

        [TestMethod]
        public void AnalyticsSubmitPackageByNetworkWithData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = LoadTestData(@"\TestData\AnalyticsData_0.json");

            var data = JsonConvert.DeserializeObject<Package>(json);

            var response = client.PostAsJsonAsync("submitpackage", data).Result;
            if (!response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync();
                Assert.Fail(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }
            else
            {
                var res = response.Content.ReadAsAsync<bool>();
                Assert.IsTrue(res.Result);
            }
        }

        private string LoadTestData(string filePath)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\" + filePath;
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return string.Empty;
        }
    }
}
