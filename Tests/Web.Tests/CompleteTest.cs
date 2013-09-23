using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void UserGeneralProcessByNetworkMethod()
        {
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = _baseAddress })
            {
                // Log in
                var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("UserName", "test@appreadygo.com"),
                    new KeyValuePair<string, string>("Password", "111111")
                });
                var result = client.PostAsync("/Account/LogOn", data).Result;
                if (!result.IsSuccessStatusCode)
                {
                    var res = result.Content.ReadAsStringAsync().Result;
                    Assert.Fail(string.Format("{0} ({1}) Body:{2}", (int)result.StatusCode, result.ReasonPhrase, res));
                }
                else
                {
                    var res = result.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(res.Contains("<h4>New Application</h4>"), res);
                }

                // Application New
                data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("Name", "Test Application"),
                    new KeyValuePair<string, string>("Description", "Test application"),
                    new KeyValuePair<string, string>("Type", "1")
                });
                result = client.PostAsync("/Application/New", data).Result;
                if (!result.IsSuccessStatusCode)
                {
                    var res = result.Content.ReadAsStringAsync().Result;
                    Assert.Fail(string.Format("{0} ({1}) Body:{2}", (int)result.StatusCode, result.ReasonPhrase, res));
                }
                else
                {
                    var res = result.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(res.Contains("<h4>New Application</h4>"), res);
                }
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
    }
}
