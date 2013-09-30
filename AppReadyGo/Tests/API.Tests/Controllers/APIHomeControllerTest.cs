using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.API.Models.Market;
using AppReadyGo.Common.Tests;
using AppReadyGo.Core.Entities;
using Common.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppReadyGo.API.Tests.Controllers
{
    [TestClass]
    public class APIHomeControllerTest
    {
        [TestMethod]
        public void ResetPasswordByNetwork()
        {
            string userName = "ypanshin+api" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@gmail.com";

            var data = new UserModel { ContryId = 4, Gender = Gender.Women, AgeRange = AgeRange.Range35_44, Zip = "NW42RX", Email = userName, FirstName = "xxx", Password = "111111" };
            MarketByNetwork.Register(data);

            APIByNetwork.Activate(data.Email);

            APIByNetwork.ResetPassword(data.Email, "222222");

            MarketByNetwork.LogOn(data.Email, "222222");
        }
    }
}
