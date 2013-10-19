using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AppReadyGo.Core.Entities;
using AppReadyGo.Web.Common.Mails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Core;
using System.Net;

namespace Common.Tests
{
    public static class WebByNetwork
    {
        public static string Register(string username, string password, bool validateResult = true)
        {
            using (var client = new HttpClient() { BaseAddress = Global.BaseAddress })
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
                    Assert.Fail(string.Format("Web Register - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                }
                else if(validateResult)
                {
                    Assert.IsTrue(res.Contains("Activation email was sent"), string.Format("Web Register - wrong responce: {0}", res));
                }

                return res;
            }
        }

        public static void Activate(string username)
        {
            using (var client = new HttpClient() { BaseAddress = Global.BaseAddress })
            {
                string key = string.Format("{0},{1}", DateTime.Now.AddDays(14).ToString(ActivationEmail.DateFormat), username).EncryptLow(); ;
                var responce = client.GetAsync("Account/Activate/?key=" + HttpUtility.UrlEncode(key)).Result;
                var res = responce.Content.ReadAsStringAsync().Result;

                if (!responce.IsSuccessStatusCode)
                {
                    Assert.Fail(string.Format("Web Activate - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                }
                else
                {
                    Assert.IsTrue(res.Contains("<h2>Your account was activated</h2>"), string.Format("Web Activate - wrong responce: {0}", res));
                }
            }
        }

        public static string LogOn(string username, string password, bool validateResult = true)
        {
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = Global.BaseAddress })
            {
                return LogOn(client, username, password, validateResult);
            }
        }

        public static string LogOn(HttpClient client, string username, string password, bool validateResult = true)
        {
            var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("UserName", username),
                    new KeyValuePair<string, string>("Password", password)
                });
            var responce = client.PostAsync("/Account/LogOn", data).Result;
            var res = responce.Content.ReadAsStringAsync().Result;

            if (!responce.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("Web LogOn - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
            }
            else if(validateResult)
            {
                Assert.IsTrue(res.Contains("<title>AppReadyGo -   Dashboard  </title>"), string.Format("Web LogOn - wrong responce: {0}", res));
            }

            return res;
        }

        public static int ApplicationNew(HttpClient client, string name = null, string desc = null, int type = 1, string basePath = @"\..\..\Resources\")
        {
            name = name ?? "WhatsApp Messenger " + DateTime.Now.ToString("yyyyMMddHHmmss");
            desc = desc ?? @"WhatsApp Messenger is a smartphone messenger available for Android and other smartphones. WhatsApp uses your 3G or WiFi (when available) to message with friends and family. Switch from SMS to WhatsApp to send and receive messages, pictures, audio notes, and video messages. First year FREE! ($0.99 USD/year after).";
            string packagePath = AppDomain.CurrentDomain.BaseDirectory + basePath + "WhatsApp.apk";
            string iconPath = AppDomain.CurrentDomain.BaseDirectory + basePath + "Icon1.png";
            using (var data = new MultipartFormDataContent())
            {
                var values = new[]
                {
                    new KeyValuePair<string, string>("Name", name),
                    new KeyValuePair<string, string>("Description", desc),
                    new KeyValuePair<string, string>("Type", type.ToString())
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
                        Assert.Fail(string.Format("Web Application New - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                    }
                    else
                    {
                        Assert.IsTrue(res.Contains("<a href=\"/Application/Edit/"), string.Format("Web Application New - wrong responce: {0}", res));
                    }

                    int startIndx = res.LastIndexOf("<a href=\"/Application/Edit/") + 27;
                    int endIndx = res.IndexOf("\"", startIndx);
                    return int.Parse(res.Substring(startIndx, endIndx - startIndx));
                }
            }
        }

        public static StreamContent CreateFileContent(Stream stream, string key, string fileName, string contentType)
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

        public static void ApplicationPublish(HttpClient client, int appId, AgeRange ageRange, Gender gender, int country, string zip)
        {
            var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                    new KeyValuePair<string, string>("ApplicationId", appId.ToString()),
                    new KeyValuePair<string, string>("AgeRange", ((int)ageRange).ToString()),
                    new KeyValuePair<string, string>("Gender", ((int)gender).ToString()),
                    new KeyValuePair<string, string>("Country", country.ToString()),
                    new KeyValuePair<string, string>("Zip", zip)
                });
            var responce = client.PostAsync("/Application/Publish/" + appId, data).Result;
            var res = responce.Content.ReadAsStringAsync().Result;

            if (!responce.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("Web Application Publish - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
            }
            else
            {
                Assert.IsTrue(res.Contains("<a href=\"/Application/Edit/"), string.Format("Web Application Publish - wrong responce: {0}", res));
            }
        }

        public static Tuple<int, string, int, int>[] ApplicationDashboard(HttpClient client)
        {
            var responce = client.GetAsync("/Application/").Result;
            var res = responce.Content.ReadAsStringAsync().Result;

            if (!responce.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("Web Application Index - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
            }
            else
            {
                Assert.IsTrue(res.Contains("<a href=\"/Application/Edit/"), string.Format("Web Application Publish - wrong responce: {0}", res));
            }

            Tuple<int, string, int, int> item = null;
            int indx = 0;
            var list = new List<Tuple<int, string, int, int>>();
            while ((indx = GetDashboardRow(res, out item, indx)) > 0)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        private static int GetDashboardRow(string responce, out Tuple<int, string, int, int> item, int startIndx = 0)
        {
            item = null;

            int indx = responce.IndexOf("<tr class=\"portf alt\" appid=\"", startIndx);

            if (indx < 0) return -1;

            indx += 29;
            int endIdx = responce.IndexOf("\"", indx);
            int id = int.Parse(responce.Substring(indx, endIdx - indx));
            indx = responce.IndexOf("<div class=\"status-", indx) + 19;
            endIdx = responce.IndexOf("\"", indx);
            string status = responce.Substring(indx, endIdx - indx);
            indx = responce.IndexOf("Downloaded: ", indx) + 12;
            endIdx = responce.IndexOf("&nbsp;", indx);
            int downloaded = int.Parse(responce.Substring(indx, endIdx - indx).Trim());
            indx = responce.IndexOf("Live visits: ", indx) + 13;
            endIdx = responce.IndexOf("</td>", indx);
            int visits = int.Parse(responce.Substring(indx, endIdx - indx).Trim());

            item = new Tuple<int, string, int, int>(id, status, downloaded, visits);
            
            return endIdx;
        }

        public static void ApplicationRemove(HttpClient client, int appId_1)
        {
            var responce = client.GetAsync("/Application/Remove/" + appId_1).Result;
            var res = responce.Content.ReadAsStringAsync().Result;

            if (!responce.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("Web Application Remove - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
            }
            else
            {
                Assert.IsTrue(res.Contains("<h4>New Application</h4>"), string.Format("Web Application Remove - wrong responce: {0}", res));
            }
        }

        public static void LogOff(HttpClient client)
        {
            var responce = client.GetAsync("/Account/LogOff").Result;
            var res = responce.Content.ReadAsStringAsync().Result;

            if (!responce.IsSuccessStatusCode)
            {
                Assert.Fail(string.Format("Web Account LogOff - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
            }
            else
            {
                Assert.IsTrue(res.Contains("<title>AppReadyGo -   Home  </title>"), string.Format("Web Account LogOff - wrong responce: {0}", res));
            }
        }
    }
}
