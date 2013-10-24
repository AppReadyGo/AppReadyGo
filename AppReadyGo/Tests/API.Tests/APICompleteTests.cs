using System;
using AppReadyGo.Common.Tests;
using Common.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppReadyGo.API.Tests
{
    [TestClass]
    public class APICompleteTests
    {
        [TestMethod]
        public void APICompleteTest()
        {
            string userName = "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string apiUserName = "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string api1UserName = "ypanshin+api1" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";
            string password = "111111";

            MarketByNetwork.GetSettings();

            MarketByNetwork.Register(apiUserName, password);

            APIByNetwork.Activate(apiUserName);

            MarketByNetwork.LogOn(apiUserName, password);

            var res = MarketByNetwork.RegisterThirdParty(apiUserName);

            MarketByNetwork.ForgotPassword(apiUserName);

            MarketByNetwork.Update(res.Id.Value, apiUserName);

            var apps = MarketByNetwork.GetApps(res.Id.Value);
            var appId = apps.Collection[0].Id;
            //MarketByNetwork.GetApp(res.Id.Value, appId);

            MarketByNetwork.Used(res.Id.Value, appId);

            MarketByNetwork.UpdateReview(res.Id.Value, appId);

            MarketByNetwork.RegisterThirdParty(api1UserName);
        }
    }
}
