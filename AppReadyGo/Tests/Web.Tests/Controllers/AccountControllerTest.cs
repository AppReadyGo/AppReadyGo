using System;
using System.Collections.Generic;
using System.Net.Http;
using AppReadyGo.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppReadyGo.Web.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
#if QA
        static readonly Uri _baseAddress = new Uri("http://qa.appreadygo.com/");
#elif DEBUG
        static readonly Uri _baseAddress = new Uri("http://localhost:63224/");
#else
        static readonly Uri _baseAddress = new Uri("http://appreadygo.com/");
#endif

        [TestMethod]
        public void AccountControllerNetworkRegistration()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("Email", "ypanshin+web" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com"),
                new KeyValuePair<string, string>("Password", "123456"),
                new KeyValuePair<string, string>("ConfirmPassword", "123456")
            });

            var task = client.PostAsync("Account/Register", data);
            var response = task.Result;
            if (!response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                Assert.Fail(string.Format("{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, res));
            }
            else
            {
                var res = response.Content.ReadAsStringAsync().Result;
                Assert.IsTrue(res.Contains("Activation email was sent"), res);
            }
        }
    }
}
