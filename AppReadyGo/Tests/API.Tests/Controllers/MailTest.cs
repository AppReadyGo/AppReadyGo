using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Web.Common.Mails;
using AppReadyGo.Common;

namespace AppReadyGo.API.Tests.Controllers
{
    [TestClass]
    public class MailTest
    {
        [TestMethod]
        public void ActivationEmailTest()
        {
            ObjectContainer.Instance.GetType();
            new MailGenerator().Send(new ActivationEmail("ypanshin@gmail.com", "/Activate/", "http://api.qa.appreadygo.com"));
        }
    }
}
