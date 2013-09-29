using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.API.Models.Market;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    public static class MarketByNetwork
    {
        public static void Register(string email = null, string password = null)
        {
            email = email ?? "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            password = password ?? "111111";
            var data = new UserModel { ContryId = 1, Email = email, FirstName = "xxx", Password = password };
            Register(data);
        }

        public static void Register(UserModel data)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = Global.ApiBaseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


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

        public static int? LogOn(string email, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = Global.ApiBaseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var regdata = new UserModel { ContryId = 1, Email = email, FirstName = "xxx", Password = password };

            var task = client.PostAsJsonAsync("register", regdata);


            var data = new LoginModel { Email = regdata.Email, Password = regdata.Password };

            var response = client.PostAsJsonAsync("login", data).Result;
            if (!response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync();
                Assert.Fail(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

                return null;
            }
            else
            {
                var res = response.Content.ReadAsAsync<UserResultModel>().Result;
                Assert.AreEqual(res.Code, UserResultModel.Result.Successful);

                return res.Id;
            }
        }
    }
}
