using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AppReadyGo.Web.Common.Mails;
using Common.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Core;

namespace AppReadyGo.Web.Tests
{
    public static class APIByNetwork
    {

        internal static void Activate(string apiUserName)
        {
            using (var client = new HttpClient() { BaseAddress = Global.ApiBaseAddress })
            {
                string key = string.Format("{0},{1}", DateTime.Now.AddDays(14).ToString(ActivationEmail.DateFormat), apiUserName).EncryptLow(); ;
                var responce = client.GetAsync("/Activate/?key=" + HttpUtility.UrlEncode(key)).Result;
                var res = responce.Content.ReadAsStringAsync().Result;

                if (!responce.IsSuccessStatusCode)
                {
                    Assert.Fail(string.Format("API Activate - Fatal error:{0} ({1}) Body:{2}", (int)responce.StatusCode, responce.ReasonPhrase, res));
                }
                else
                {
                    Assert.IsTrue(res.Contains("<title> Your account was activated </title>"), string.Format("API Activate - wrong responce: {0}", res));
                }
            }
        }
    }
}
