using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Web.Common.Mails;
using AppReadyGo.Common;
using AppReadyGo.API.Common.Mails;

namespace AppReadyGo.API.Tests.Controllers
{
    [TestClass]
    public class MailTest
    {
        [TestMethod]
        public void ActivationEmailTest()
        {
            ObjectContainer.Instance.GetType();
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\..\API\";
            new APIActivationEmail("ypanshin@gmail.com", "http://api.qa.appreadygo.com", path).Send();
        }
    }
}
