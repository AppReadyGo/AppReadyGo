using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.API.Models.Analytics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Common.Tests
{
    public static class AnalyticsByNetwork
    {
        public static void SubmitPackageByNetwork(int appId, int width, int height)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = Global.ApiBaseAddress;
                client.Timeout = new TimeSpan(0, 3, 0);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var data = AnalyticsByNetwork.CreateRandomPackage(appId, width, height);

                string json = JsonConvert.SerializeObject(data);


                var response = client.PostAsJsonAsync("/analytics/submitpackage", data).Result;
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
        }

        public static Package CreateRandomPackage(int appId, int width, int height)
        {
            var rnd = new Random();

            var data = new Package
            {
                ClientKey = "123-" + appId.ToString("0000"),
                ScreenHeight = height,
                ScreenWidth = width,
                SystemInfo = new SystemInfo
                {
                }
            };

            var sessionInfos = new List<SessionInfo>();
            for (int i = 0; i < 5; i++)
            {
                var session = new SessionInfo()
                {
                    ClientHeight = height,
                    ClientWidth = width,
                    PageUri = "Screen_" + i,
                };
                var touchDetails = new List<TouchDetails>();
                for (int y = 0; y < 10; y++)
                {
                    touchDetails.Add(new TouchDetails
                    {
                        ClientX = rnd.Next(0, width),
                        ClientY = rnd.Next(0, height),
                        Date = DateTime.UtcNow.AddSeconds((i * 10) + y),
                        Orientation = 1
                    });
                }
                session.TouchDetails = touchDetails.ToArray();
                var scrollDetails = new List<ScrollDetails>();
                for (int y = 0; y < 10; y++)
                {
                    scrollDetails.Add(new ScrollDetails
                    {
                        StartTouchData = new TouchDetails
                        {
                            ClientX = rnd.Next(0, width),
                            ClientY = rnd.Next(0, height),
                            Date = DateTime.UtcNow.AddSeconds((i * 10) + y),
                            Orientation = 1
                        },
                        CloseTouchData = new TouchDetails
                        {
                            ClientX = rnd.Next(0, width),
                            ClientY = rnd.Next(0, height),
                            Date = DateTime.UtcNow.AddSeconds((i * 10) + y),
                            Orientation = 1
                        }
                    });
                }
                session.ScrollDetails = scrollDetails.ToArray();
                var viewAreaDetails = new List<ViewAreaDetails>();
                for (int y = 0; y < 10; y++)
                {
                    viewAreaDetails.Add(new ViewAreaDetails
                    {
                        CoordX = 0,
                        CoordY = rnd.Next(0, height),
                        StartDate = DateTime.UtcNow.AddSeconds((i * 10) + y),
                        FinishDate = DateTime.UtcNow.AddSeconds((i * 10) + y + rnd.Next(1, 10)),
                        Orientation = 1
                    });
                }
                session.ViewAreaDetails = viewAreaDetails.ToArray();
                var controlClickDetails = new List<ControlClickDetails>();
                for (int y = 0; y < 10; y++)
                {
                    controlClickDetails.Add(new ControlClickDetails
                    {
                        ControlTag = "some" + y,
                        Date = DateTime.UtcNow.AddSeconds((i * 10) + y)
                    });
                }

                session.ControlClickDetails = controlClickDetails.ToArray();
                sessionInfos.Add(session);
            }

            data.SessionsInfo = sessionInfos.ToArray();

            return data;
        }
    }
}
