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
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var regdata = new UserModel { ContryId = 1, Email = "pmogilev@appreadygo.com", FirstName = "xxx", Password = "qweasd" };

            var task = client.PostAsJsonAsync("register", regdata);


            var data = new LoginModel { Email = regdata.Email, Password = regdata.Password };

            var response = client.PostAsJsonAsync("login", data).Result;
            if (!response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync();
                Assert.Fail(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
            }
            else
            {
                var res = response.Content.ReadAsAsync<UserResultModel>().Result;
                Assert.AreEqual(res.Code, UserResultModel.Result.Successful);
            }
        }

        [TestMethod]
        public void MarketRegisterByNetwork()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new UserModel { ContryId = 1, Email = "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com", FirstName = "xxx", Password = "1234" };

            var task = client.PostAsJsonAsync("register", data);
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
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = client.GetAsync("GetApps/?email=some&curPage=1&pageSize=10").Result;
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
    }
}
