using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.API.Models.Market;
using AppReadyGo.Common.Mails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Core;
using Newtonsoft.Json;
using AppReadyGo.Core.Entities;

namespace Common.Tests
{
    public static class MarketByNetwork
    {
        public static void GetSettings()
        {
            using (var client = new HttpClient() { BaseAddress = Global.ApiBaseAddress })
            {            
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = client.GetAsync("/market/getsettings").Result;
                if (!response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync();
                    Assert.Fail(string.Format("API MarketGetSettings - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, res));
                }
                else
                {
                    var res = response.Content.ReadAsAsync<SettingsModel>().Result;
                    Assert.IsFalse(string.IsNullOrEmpty(res.WebApplicationBaseURL));
                }
            }
        }

        public static RegisterResultModel Register(string email = null, string password = null, bool validateResult = true)
        {
            email = email ?? "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            password = password ?? "111111";
            var data = new UserModel { ContryId = 1, Email = email, FirstName = "xxx", Password = password };
            return Register(data, validateResult);
        }

        public static RegisterResultModel Register(UserModel data, bool validateResult = true)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = Global.ApiBaseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var task = client.PostAsJsonAsync("/market/register", data);
            var response = task.Result;
            if (!response.IsSuccessStatusCode)
            {
                var resStr = response.Content.ReadAsStringAsync().Result;
                Assert.Fail(string.Format("API Register - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, resStr));

                return null;
            }

            var res = response.Content.ReadAsAsync<RegisterResultModel>().Result;
            if (validateResult)
            {
                Assert.IsTrue(res.Id.HasValue);
            }
            return res;
        }

        public static UserResultModel LogOn(string email, string password, bool validateResult = true)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = Global.ApiBaseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new LoginModel { Email = email, Password = password };

            var response = client.PostAsJsonAsync("/market/login", data).Result;
            if (!response.IsSuccessStatusCode)
            {
                var resStr = response.Content.ReadAsStringAsync();
                Assert.Fail(string.Format("API LogOn - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, resStr));

                return null;
            }

            var res = response.Content.ReadAsAsync<UserResultModel>().Result;
            if (validateResult)
            {
                Assert.AreEqual(res.Code, UserResultModel.Result.Successful);
            }

            return res; 
        }

        public static PaggingModel<ApplicationModel> GetApps(int userId, int page = 1, int pagesize = 100000)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = Global.ApiBaseAddress;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync("/market/getapps/?userId=" + userId + "&curPage=" + page + "&pageSize=" + pagesize).Result;
            if (!response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync();
                Assert.Fail(string.Format("API GetApps - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, res));

                return null;
            }
            else
            {
                var res = response.Content.ReadAsAsync<PaggingModel<ApplicationModel>>().Result;
                return res;
            }
        }

        public static void GetApp(int userId, int appId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = Global.ApiBaseAddress;

                var response = client.GetByteArrayAsync("/market/getapp/?userId=" + userId + "&appId=" + appId);
                Assert.IsTrue(response.IsFaulted, "API GetApp - Fatal error: the response is faulted");
                Assert.IsTrue(response.Result.Length > 0, "API GetApp - Fatal error: the result length is zero");
            }

        }

        public static RegisterResultModel RegisterThirdParty(string email, bool validateResult = true)
        {
            email = email ?? "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            var data = new ThirdPartyUserModel { ContryId = 1, Email = email, FirstName = "xxx" };
            return RegisterThirdParty(data, validateResult);
        }

        public static RegisterResultModel RegisterThirdParty(ThirdPartyUserModel data, bool validateResult = true)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = Global.ApiBaseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var task = client.PostAsJsonAsync("/market/thirdpartyregister", data);
            var response = task.Result;
            if (!response.IsSuccessStatusCode)
            {
                var resStr = response.Content.ReadAsStringAsync().Result;
                Assert.Fail(string.Format("API RegisterThirdParty - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, resStr));

                return null;
            }

            var res = response.Content.ReadAsAsync<RegisterResultModel>().Result;
            if (validateResult)
            {
                Assert.IsTrue(res.Id.HasValue);
            }
            return res;
        }

        public static ResultCode ForgotPassword(string apiUserName, bool validateResult = true)
        {
            using (var client = new HttpClient() { BaseAddress = Global.ApiBaseAddress })
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var task = client.PostAsJsonAsync("/market/ForgotPassword", apiUserName);
                var response = task.Result;
                if (!response.IsSuccessStatusCode)
                {
                    var resStr = response.Content.ReadAsStringAsync().Result;
                    Assert.Fail(string.Format("API ForgotPassword - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, resStr));

                    return ResultCode.MissingData;
                }

                var res = response.Content.ReadAsAsync<ResultCode>().Result;
                if (validateResult)
                {
                    Assert.AreEqual(ResultCode.Successful, res);
                }
                return res;
            }
        }

        public static ResultCode Update(int userId, string apiUserName, bool validateResult = true)
        {
            using (var client = new HttpClient() { BaseAddress = Global.ApiBaseAddress })
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var model = new UserModel { Id = userId, Email = apiUserName, AgeRange = AgeRange.None };

                var task = client.PostAsJsonAsync("/market/updateuser", model);
                var response = task.Result;
                if (!response.IsSuccessStatusCode)
                {
                    var resStr = response.Content.ReadAsStringAsync().Result;
                    Assert.Fail(string.Format("API Update - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, resStr));

                    return ResultCode.MissingData;
                }

                var res = response.Content.ReadAsAsync<ResultCode>().Result;
                if (validateResult)
                {
                    Assert.AreEqual(ResultCode.Successful, res);
                }
                return res;
            }
        }

        public static void Used(int userId, int appId)
        {
            using (var client = new HttpClient() { BaseAddress = Global.ApiBaseAddress })
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var model = new TaskModel { TaskId = appId, UserId = userId };

                var task = client.PostAsJsonAsync("/market/used", model);
                var response = task.Result;
                if (!response.IsSuccessStatusCode)
                {
                    var resStr = response.Content.ReadAsStringAsync().Result;
                    Assert.Fail(string.Format("API Used - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, resStr));
                }
            }
        }

        public static void UpdateReview(int userId, int appId)
        {
            using (var client = new HttpClient() { BaseAddress = Global.ApiBaseAddress })
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var model = new ReviewModel { TaskId = appId, UserId = userId, Review = "Some review" };

                var task = client.PostAsJsonAsync("/market/updatereview", model);
                var response = task.Result;
                if (!response.IsSuccessStatusCode)
                {
                    var resStr = response.Content.ReadAsStringAsync().Result;
                    Assert.Fail(string.Format("API Update Review - Fatal error:{0} ({1}) Body:{2}", (int)response.StatusCode, response.ReasonPhrase, resStr));
                }
            }
        }
    }
}
