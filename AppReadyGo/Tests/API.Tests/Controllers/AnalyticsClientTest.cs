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
            var data = CreatePackage();
            var controller = new AnalyticsController();
            var view = controller.SubmitPackage(data);
            Assert.IsTrue(view);
        }

        [TestMethod]
        public void AnalyticsSubmitPackageByNetwork()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = CreatePackage();

            string json = JsonConvert.SerializeObject(data);


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

        [TestMethod]
        public void AnalyticsSubmitPackageByNetworkWithData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = "{\"ssd\":[{\"sda\":[],\"vwa\":[{\"sd\":\"Sun, 07 Jul 2013 21:46:39 GMT\",\"o\":0,\"cy\":0,\"cx\":0,\"fd\":\"Sun, 07 Jul 2013 21:46:43 GMT\"}],\"sc\":\"Sun, 07 Jul 2013 21:46:43 GMT\",\"ss\":\"Sun, 07 Jul 2013 21:46:39 GMT\",\"tda\":[{\"o\":0,\"cy\":525,\"d\":\"Sun, 07 Jul 2013 21:46:41 GMT\",\"cx\":42,\"p\":500}],\"cw\":320,\"ch\":533,\"uri\":\"LoginActivity\"},{\"sda\":[],\"vwa\":[{\"sd\":\"Sun, 07 Jul 2013 21:46:43 GMT\",\"o\":0,\"cy\":0,\"cx\":0,\"fd\":\"Sun, 07 Jul 2013 21:48:45 GMT\"}],\"sc\":\"Sun, 07 Jul 2013 21:48:45 GMT\",\"ss\":\"Sun, 07 Jul 2013 21:46:43 GMT\",\"tda\":[{\"o\":0,\"cy\":570,\"d\":\"Sun, 07 Jul 2013 21:48:41 GMT\",\"cx\":38,\"p\":250},{\"o\":0,\"cy\":531,\"d\":\"Sun, 07 Jul 2013 21:48:43 GMT\",\"cx\":200,\"p\":375}],\"cw\":320,\"ch\":533,\"uri\":\"LoginActivity\"},{\"sda\":[],\"vwa\":[{\"sd\":\"Mon, 08 Jul 2013 16:28:18 GMT\",\"o\":0,\"cy\":0,\"cx\":0,\"fd\":\"Mon, 08 Jul 2013 16:28:23 GMT\"}],\"sc\":\"Mon, 08 Jul 2013 16:28:23 GMT\",\"ss\":\"Mon, 08 Jul 2013 16:28:18 GMT\",\"tda\":[{\"o\":0,\"cy\":520,\"d\":\"Mon, 08 Jul 2013 16:28:21 GMT\",\"cx\":219,\"p\":375}],\"cw\":320,\"ch\":533,\"uri\":\"LoginActivity\"},{\"sda\":[],\"vwa\":[{\"sd\":\"Sat, 13 Jul 2013 15:41:19 GMT\",\"o\":0,\"cy\":0,\"cx\":0,\"fd\":\"Sat, 13 Jul 2013 15:41:24 GMT\"}],\"sc\":\"Sat, 13 Jul 2013 15:41:24 GMT\",\"ss\":\"Sat, 13 Jul 2013 15:41:19 GMT\",\"tda\":[{\"o\":0,\"cy\":538,\"d\":\"Sat, 13 Jul 2013 15:41:22 GMT\",\"cx\":219,\"p\":500}],\"cw\":320,\"ch\":533,\"uri\":\"LoginActivity\"},{\"sda\":[],\"vwa\":[{\"sd\":\"Sat, 13 Jul 2013 15:41:26 GMT\",\"o\":0,\"cy\":0,\"cx\":0,\"fd\":\"Sat, 13 Jul 2013 15:43:14 GMT\"}],\"sc\":\"Sat, 13 Jul 2013 15:43:14 GMT\",\"ss\":\"Sat, 13 Jul 2013 15:41:26 GMT\",\"tda\":[{\"o\":0,\"cy\":540,\"d\":\"Sat, 13 Jul 2013 15:43:12 GMT\",\"cx\":200,\"p\":250}],\"cw\":320,\"ch\":533,\"uri\":\"LoginActivity\"}],\"sh\":533,\"cid\":\"MA-0048-00001\",\"sw\":320,\"ssi\":{\"con\":\"REL\",\"app\":\"1.0\",\"opn\":\"42503\",\"han\":\"JZO54K\",\"sdki\":16,\"mon\":\"GT-I9100T\",\"fin\":\"samsung\\/GT-I9100T\\/GT-I9100T:4.1.2\\/JZO54K\\/I9100TJJLS4:user\\/release-keys\",\"din\":\"JZO54K.I9100TJJLS4\",\"inc\":\"I9100TJJLS4\",\"brn\":\"samsung\",\"jar\":\"1-9.26\",\"prn\":\"GT-I9100T\",\"rel\":\"4.1.2\",\"den\":\"GT-I9100T\",\"man\":\"samsung\"}}";
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

        private static Package CreatePackage()
        {
            var data = new Package
            {
                ClientKey = "123-123-0001",
                ScreenHeight = 100,
                ScreenWidth = 200,
                SystemInfo = new SystemInfo
                {
                },
                SessionsInfo = new SessionInfo[]
                {
                    new SessionInfo
                    {
                        ClientHeight = 100,
                        ClientWidth = 200,
                        PageUri = "home",
                        TouchDetails = new TouchDetails[]
                        {
                            new TouchDetails
                            {
                                ClientX = 50,
                                ClientY = 40,
                                Date = DateTime.UtcNow.AddMinutes(-1),
                                Orientation = 1
                            },
                            new TouchDetails
                            {
                                ClientX = 40,
                                ClientY = 30,
                                Date = DateTime.UtcNow.AddSeconds(-30),
                                Orientation = 1
                            }
                        },
                        ScrollDetails = new ScrollDetails[]
                        {
                            new ScrollDetails
                            {
                                // TODO: Yura: Why do we need all the data? lets take the data from first and last touch in touch details array
                                CloseTouchData = new TouchDetails
                                {
                                    ClientX = 40,
                                    ClientY = 30,
                                    Date = DateTime.UtcNow.AddSeconds(-30),
                                    Orientation = 1
                                },
                                StartTouchData = new TouchDetails
                                {
                                    ClientX = 50,
                                    ClientY = 40,
                                    Date = DateTime.UtcNow.AddMinutes(-1),
                                    Orientation = 1
                                }
                            }
                        },
                        ViewAreaDetails = new ViewAreaDetails[]
                        {
                            new ViewAreaDetails
                            {
                                    CoordX = 0,
                                    CoordY = 20,
                                    StartDate = DateTime.UtcNow.AddSeconds(-30),
                                    FinishDate = DateTime.UtcNow.AddSeconds(-25)
                            }
                        }
                    }
                }
            };
            
            return data;
        }
    }
}
