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
        static readonly Uri _baseAddress = new Uri("http://localhost:63321/api/market/");

        [TestMethod]
        public void MarketLoginByNetwork()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new LoginModel { Email = "test@test.com", Password = "1234" };

            var response = client.PostAsJsonAsync("login", data).Result;
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
        public void MarketRegisterByNetwork()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = _baseAddress;

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new RegisterModel { ContryId = 1, Email = "test@test.com", FirstName = "xxx", Password = "121" };

            var response = client.PostAsJsonAsync("register", data).Result;
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
        public void MarketRegisterByNetwork1()
        {

            System.Net.WebRequest req = System.Net.WebRequest.Create("http://localhost:63321/api/Market/Register");
            //Add these, as we're doing a POST
            req.ContentType = "application/json";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes("{\"email\":\"test@test.com\",\"pass\":\"121\",\"firstname\":\"xxx\",\"lastname\":null,\"gender\":0,\"agerange\":0,\"countryid\":1,\"zip\":null,\"interests\":null}");
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();

            System.Net.WebResponse resp = req.GetResponse();
            //if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            var res = sr.ReadToEnd().Trim();
        }

        //[TestMethod]
        //public void AnalyticsSubmitPackage()
        //{
        //    ObjectContainer.Instance.GetType();
        //    var data = CreatePackage();
        //    var controller = new AnalyticsController();
        //    var view = controller.SubmitPackage(data);
        //    Assert.IsTrue(view);
        //}

        //[TestMethod]
        //public void AnalyticsSubmitPackageByNetwork()
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = _baseAddress;

        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var data = CreatePackage();

        //    var response = client.PostAsJsonAsync("submitpackage", data).Result;
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var res = response.Content.ReadAsStringAsync();
        //        Assert.Fail(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));
        //    }
        //    else
        //    {
        //        var res = response.Content.ReadAsAsync<bool>();
        //        Assert.IsTrue(res.Result);
        //    }
        //}

        //private static FingerPrintData CreatePackage()
        //{
        //    var data = new FingerPrintData
        //    {
        //        Ip = "0.0.0.1",
        //        Package = new Package
        //        {
        //            ClientKey = "123-123-0001",
        //            ScreenHeight = 100,
        //            ScreenWidth = 200,
        //            SystemInfo = new SystemInfo
        //            {
        //            },
        //            SessionsInfo = new SessionInfo[]
        //            {
        //                new SessionInfo
        //                {
        //                    ClientHeight = 100,
        //                    ClientWidth = 200,
        //                    PageUri = "home",
        //                    TouchDetails = new TouchDetails[]
        //                    {
        //                        new TouchDetails
        //                        {
        //                            ClientX = 50,
        //                            ClientY = 40,
        //                            Date = DateTime.UtcNow.AddMinutes(-1),
        //                            Orientation = 1
        //                        },
        //                        new TouchDetails
        //                        {
        //                            ClientX = 40,
        //                            ClientY = 30,
        //                            Date = DateTime.UtcNow.AddSeconds(-30),
        //                            Orientation = 1
        //                        }
        //                    },
        //                    ScrollDetails = new ScrollDetails[]
        //                    {
        //                        new ScrollDetails
        //                        {
        //                            // TODO: Yura: Why do we need all the data? lets take the data from first and last touch in touch details array
        //                            CloseTouchData = new TouchDetails
        //                            {
        //                                ClientX = 40,
        //                                ClientY = 30,
        //                                Date = DateTime.UtcNow.AddSeconds(-30),
        //                                Orientation = 1
        //                            },
        //                            StartTouchData = new TouchDetails
        //                            {
        //                                ClientX = 50,
        //                                ClientY = 40,
        //                                Date = DateTime.UtcNow.AddMinutes(-1),
        //                                Orientation = 1
        //                            }
        //                        }
        //                    },
        //                    ViewAreaDetails = new ViewAreaDetails[]
        //                    {
        //                        new ViewAreaDetails
        //                        {
        //                             CoordX = 0,
        //                             CoordY = 20,
        //                             StartDate = DateTime.UtcNow.AddSeconds(-30),
        //                             FinishDate = DateTime.UtcNow.AddSeconds(-25)
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    return data;
        //}
    }
}
