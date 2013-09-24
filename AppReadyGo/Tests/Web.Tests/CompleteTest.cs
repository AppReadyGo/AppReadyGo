using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Core;
using System.Web;

namespace AppReadyGo.Web.Tests
{
    [TestClass]
    public class CompleteTest
    {
#if QA
        static readonly Uri _baseAddress = new Uri("http://qa.appreadygo.com/");
#elif DEBUG
        static readonly Uri _baseAddress = new Uri("http://localhost:63224/");
#else
        static readonly Uri _baseAddress = new Uri("http://appreadygo.com/");
#endif
        
        [TestMethod]
        public void UserFlowByNetworkMethod()
        {
            string userName = "ypanshin+" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string password = "111111";

            Register(userName, password);
            Activate(userName);

            // After login section
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = _baseAddress })
            {
                LogOn(client, userName, password);

                ApplicationNew(client);
            }
            // Application Index
            // Screen New
            // Screen Edit
            // Screen New
            // Application Publish
            // API Market download
            // API Analytics using
            // Analytics Dashboard
            // Analytics Usage
            // Analytics TouchMap
            // Analytics ABCompare
            // Analytics EyeTracker
            // Analytics ClickHeatMapImage
            // Analytics ViewHeatMapImage
            // Analytics ViewHeatMapImage
            // Application Edit
            // Screen Remove
            // Application Remove
            // Log Out
        }

        private void Register(string username, string password)
        {
            using (var client = new HttpClient() { BaseAddress = _baseAddress })
            {
                var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("Email", username),
                    new KeyValuePair<string, string>("Password", password),
                    new KeyValuePair<string, string>("ConfirmPassword", password)
                });

                var responce = client.PostAsync("/Account/Register", data).Result;
                var res = responce.Content.ReadAsStringAsync().Result;

                if (!responce.IsSuccessStatusCode)
                {
                    Assert.Fail(string.Format("Register - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                }
                else
                {
                    Assert.IsTrue(res.Contains("Activation email was sent"), string.Format("Register - wrong responce: {0}", res));
                }
            }
        }

        private void Activate(string username)
        {
            using (var client = new HttpClient() { BaseAddress = _baseAddress })
            {
                string key = string.Format("{0},{1}", DateTime.Now.AddDays(-14), username).EncryptLow(); ;
                var responce = client.GetAsync("Account/Activate/?key=" + HttpUtility.UrlEncode(key)).Result;
                var res = responce.Content.ReadAsStringAsync().Result;

                if (!responce.IsSuccessStatusCode)
                {
                    Assert.Fail(string.Format("Activate - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                }
                else
                {
                    Assert.IsTrue(res.Contains("<h2>Your account was activated</h2>"), string.Format("Activate - wrong responce: {0}", res));
                }
            }
        }

        private void LogOn(HttpClient client, string username, string password)
        {
            var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("UserName", username),
                    new KeyValuePair<string, string>("Password", password)
                });
            var responce = client.PostAsync("/Account/LogOn", data).Result;
            var res = responce.Content.ReadAsStringAsync().Result;

            if (!responce.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("LogOn - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
            }
            else
            {
                Assert.IsTrue(res.Contains("<h4>New Application</h4>"), string.Format("LogOn - wrong responce: {0}", res));
            }

        }

        private void ApplicationNew(HttpClient client)
        {
                var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("Name", "Test Application"),
                    new KeyValuePair<string, string>("Description", "Test application"),
                    new KeyValuePair<string, string>("Type", "1")
                });

                var responce = client.PostAsync("/Application/New", data).Result;
                var res = responce.Content.ReadAsStringAsync().Result;

                if (!responce.IsSuccessStatusCode)
                {
                    Assert.Fail(string.Format("Application New - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                }
                else
                {
                    Assert.IsTrue(res.Contains("<h4>New Application</h4>"), string.Format("Application New - wrong responce: {0}", res));
                }
       }
    }
}
