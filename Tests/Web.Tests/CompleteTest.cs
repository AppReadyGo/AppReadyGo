using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Core;
using System.Web;
using AppReadyGo.Common.Mails;
using AppReadyGo.Web.Common.Mails;
using System.Net.Http.Headers;
using System.IO;
using Common.Tests;
using AppReadyGo.API.Models.Market;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Web.Tests
{
    [TestClass]
    public class CompleteTest
    {
#if QA
        static readonly Uri _baseAddress = new Uri("http://qa.appreadygo.com/");
        static readonly Uri _apiBaseAddress = new Uri("http://api.qa.appreadygo.com/");
#elif DEBUG
        static readonly Uri _baseAddress = new Uri("http://localhost:63224/");
        static readonly Uri _apiBaseAddress = new Uri("");
#else
        static readonly Uri _baseAddress = new Uri("http://appreadygo.com/");
        static readonly Uri _apiBaseAddress = new Uri("http://api.appreadygo.com/");
#endif

        [TestMethod]
        public void UserFlowByNetworkMethod()
        {
            string userName = "ypanshin+web" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string apiUserName = "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string password = "111111";

            Register(userName, password);
            Activate(userName);

            // Forgot Password

            // Reset Password

            var data = new UserModel { ContryId = 4, Gender = Gender.Women, AgeRange = AgeRange.Range35_44, Zip = "NW42RX", Email = apiUserName, FirstName = "xxx", Password = password };
            MarketByNetwork.Register(data);

            MarketByNetwork.LogOn(apiUserName, password);

            // After login section
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = _baseAddress })
            {
                LogOn(client, userName, password);

                //Change password

                int appId = ApplicationNew(client);

                ApplicationEdit(client, appId);//(add screenshots)

                int screenId = ScreenNew(client, appId);

                ScreenEdit(client, appId, screenId);

                ApplicationPublish(client, appId);

            }
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
            // Screen Remove
            // Application Remove
            // Log Out
        }

        private void ApplicationPublish(HttpClient client, int appId)
        {
            var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("ApplicationId", appId.ToString()),
                    new KeyValuePair<string, string>("AgeRange", ((int)AgeRange.Range35_44).ToString()),
                    new KeyValuePair<string, string>("Gender", ((int)Gender.Women).ToString()),
                    new KeyValuePair<string, string>("Country", "4"),
                    new KeyValuePair<string, string>("Zip", "NW42RX")
                });
            var responce = client.PostAsync("/Application/Publish/" + appId, data).Result;
            var res = responce.Content.ReadAsStringAsync().Result;

            if (!responce.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("Application Publish - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
            }
            else
            {
                Assert.IsTrue(res.Contains("<a href=\"/Application/Edit/"), string.Format("Application Publish - wrong responce: {0}", res));
            }            
        }

        private void ScreenEdit(HttpClient client, int appId, int screenId)
        {
            string screenPath = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Resources\ScreenShot2.png";
            using (var data = new MultipartFormDataContent())
            {
                var values = new[]
                {
                    new KeyValuePair<string, string>("ApplicationId", appId.ToString()),
                    new KeyValuePair<string, string>("Path", "FirstScreenEdited"),
                    new KeyValuePair<string, string>("Width", "320"),
                    new KeyValuePair<string, string>("Height", "480")
                };

                foreach (var keyValuePair in values)
                {
                    data.Add(new StringContent(keyValuePair.Value), String.Format("\"{0}\"", keyValuePair.Key));
                }
                using (var screenms = new MemoryStream(File.ReadAllBytes(screenPath)))
                {
                    var fileContent = CreateFileContent(screenms, "file", Path.GetFileName(screenPath), "image/png");
                    data.Add(fileContent);

                    string prefix = "/Application/" + appId + "/Screen/";

                    var responce = client.PostAsync(prefix + "Edit/" + screenId, data).Result;
                    var res = responce.Content.ReadAsStringAsync().Result;

                    if (!responce.IsSuccessStatusCode)
                    {
                        Assert.Fail(string.Format("Screen Edit - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                    }
                    else
                    {
                        Assert.IsTrue(res.Contains("<a href=\"" + prefix + "Edit/"), string.Format("Screen Edit - wrong responce: {0}", res));
                    }
                }
            }
        }

        private int ScreenNew(HttpClient client, int appId)
        {
            string screenPath = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Resources\ScreenShot1.png";
            using (var data = new MultipartFormDataContent())
            {
                var values = new[]
                {
                    new KeyValuePair<string, string>("ApplicationId", appId.ToString()),
                    new KeyValuePair<string, string>("Path", "FirstScreen"),
                    new KeyValuePair<string, string>("Width", "320"),
                    new KeyValuePair<string, string>("Height", "480")
                };

                foreach (var keyValuePair in values)
                {
                    data.Add(new StringContent(keyValuePair.Value), String.Format("\"{0}\"", keyValuePair.Key));
                }
                using (var screenms = new MemoryStream(File.ReadAllBytes(screenPath)))
                {
                    var fileContent = CreateFileContent(screenms, "file", Path.GetFileName(screenPath), "image/png");
                    data.Add(fileContent);

                    string prefix = "/Application/" + appId + "/Screen/";

                    var responce = client.PostAsync(prefix + "New", data).Result;
                    var res = responce.Content.ReadAsStringAsync().Result;

                    if (!responce.IsSuccessStatusCode)
                    {
                        Assert.Fail(string.Format("Screen New - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                    }
                    else
                    {
                        Assert.IsTrue(res.Contains("<a href=\"" + prefix + "Edit/"), string.Format("Screen New - wrong responce: {0}", res));
                    }

                    int startIndx = res.IndexOf("<a href=\"" + prefix + "Edit/") + prefix.Length + 14;
                    int endIndx = res.IndexOf("\"", startIndx);
                    return int.Parse(res.Substring(startIndx, endIndx - startIndx));
                }
            }
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
                string key = string.Format("{0},{1}", DateTime.Now.AddDays(14).ToString(ActivationEmail.DateFormat), username).EncryptLow(); ;
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

        private int ApplicationNew(HttpClient client)
        {
            string packagePath = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Resources\WhatsApp.apk";
            string iconPath = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Resources\Icon1.png";
            using (var data = new MultipartFormDataContent())
            {
                var values = new[]
                {
                    new KeyValuePair<string, string>("Name", "WhatsApp Messenger"),
                    new KeyValuePair<string, string>("Description", @"WhatsApp Messenger is a smartphone messenger available for Android and other smartphones. WhatsApp uses your 3G or WiFi (when available) to message with friends and family. Switch from SMS to WhatsApp to send and receive messages, pictures, audio notes, and video messages. First year FREE! ($0.99 USD/year after)."),
                    new KeyValuePair<string, string>("Type", "1")
                };

                foreach (var keyValuePair in values)
                {
                    data.Add(new StringContent(keyValuePair.Value), String.Format("\"{0}\"", keyValuePair.Key));
                }
                using (var packagems = new MemoryStream(File.ReadAllBytes(packagePath)))
                using (var iconms = new MemoryStream(File.ReadAllBytes(iconPath)))
                {
                    var fileContent = CreateFileContent(packagems, "package_file", Path.GetFileName(packagePath), "application/vnd.android.package-archive");
                    data.Add(fileContent);

                    fileContent = CreateFileContent(iconms, "icon_file", Path.GetFileName(iconPath), "image/png");
                    data.Add(fileContent);

                    var responce = client.PostAsync("/Application/New", data).Result;
                    var res = responce.Content.ReadAsStringAsync().Result;

                    if (!responce.IsSuccessStatusCode)
                    {
                        Assert.Fail(string.Format("Application New - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                    }
                    else
                    {
                        Assert.IsTrue(res.Contains("<a href=\"/Application/Edit/"), string.Format("Application New - wrong responce: {0}", res));
                    }

                    int startIndx = res.IndexOf("<a href=\"/Application/Edit/") + 27;
                    int endIndx = res.IndexOf("\"", startIndx);
                    return int.Parse(res.Substring(startIndx, endIndx - startIndx));
                }
            }
        }

        private StreamContent CreateFileContent(Stream stream, string key, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"" + key + "\"",
                FileName = "\"" + fileName + "\""
            }; // the extra quotes are key here
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }

        private void ApplicationEdit(HttpClient client, int appId)
        {
            string screenShotPath = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Resources\ScreenShot{0}.png";
            using (var data = new MultipartFormDataContent())
            {
                var values = new[]
                {
                    new KeyValuePair<string, string>("Id", appId.ToString()),
                    new KeyValuePair<string, string>("Name", "WhatsApp Messenger"),
                    new KeyValuePair<string, string>("Description", @"WhatsApp Messenger is a smartphone messenger available for Android and other smartphones. WhatsApp uses your 3G or WiFi (when available) to message with friends and family. Switch from SMS to WhatsApp to send and receive messages, pictures, audio notes, and video messages. First year FREE! ($0.99 USD/year after)."),
                    new KeyValuePair<string, string>("Type", "1")
                };

                foreach (var keyValuePair in values)
                {
                    data.Add(new StringContent(keyValuePair.Value), String.Format("\"{0}\"", keyValuePair.Key));
                }
                var fileContent = CreateFileContent(new MemoryStream(), "package_file", string.Empty, "application/vnd.android.package-archive");
                data.Add(fileContent);
                fileContent = CreateFileContent(new MemoryStream(), "icon_file", string.Empty, "image/png");
                data.Add(fileContent);
                var fileStreams = new MemoryStream[4];
                for (int i = 1; i <= 4; i++)
                {
                    string filename = string.Format(screenShotPath, i);
                    fileStreams[i - 1] = new MemoryStream(File.ReadAllBytes(filename));
                    fileContent = CreateFileContent(fileStreams[i - 1], "screen_file_" + i, Path.GetFileName(filename), "image/png");
                    data.Add(fileContent);
                }

                var responce = client.PostAsync("/Application/Edit/" + appId, data).Result;
                var res = responce.Content.ReadAsStringAsync().Result;

                if (!responce.IsSuccessStatusCode)
                {
                    Assert.Fail(string.Format("Application New - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                }
                else
                {
                    Assert.IsTrue(res.Contains("<a href=\"/Application/Edit/"), string.Format("Application New - wrong responce: {0}", res));
                }

                foreach (var stream in fileStreams)
                {
                    stream.Close();
                }
            }
        }
    }
}
