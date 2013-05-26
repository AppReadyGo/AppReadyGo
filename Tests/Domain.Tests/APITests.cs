using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Domain.CommandHandlers;
using AppReadyGo.Common;
using NHibernate;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Domain.CommandHandlers.API;

namespace Domain.Tests
{
    [TestClass]
    public class APITests
    {
        private Database database;

        [TestInitialize()]
        public void TestInitialize()
        {
            this.database = new Database(@"WIN-O968SJ7A1UU\SQLEXPRESS", "arg_unittests");
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            this.database.Clear();
        }

        [TestMethod]
        public void ApplicationDownloadedCommand()
        {
            var member = new UserTests().CreateMember();
            var apiMember = new UserTests().CreateAPIMember();
            var app = new ApplicationTests().CreateApplication(member);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    new ApplicationDownloadedCommandHandler().Execute(session, new AppReadyGo.Core.Commands.API.ApplicationUsedCommand(apiMember.Id, app.Id));
                    dbTrans.Commit();
                }
            }
            Assert.AreEqual(1, apiMember.DownloadedApplications.Count());
        }

        [TestMethod]
        public void ApplicationUsedCommand()
        {
            var member = new UserTests().CreateMember();
            var apiMember = new UserTests().CreateAPIMember();
            var app = new ApplicationTests().CreateApplication(member);
            apiMember.DownloadApplication(app);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    new ApplicationUsedCommandHandler().Execute(session, new AppReadyGo.Core.Commands.API.ApplicationUsedCommand(apiMember.Id, app.Id));
                    dbTrans.Commit();
                }
            }
            Assert.IsTrue(apiMember.DownloadedApplications.First().Used);
        }

        [TestMethod]
        public void ApplicationUpdateReviewCommand()
        {
            var member = new UserTests().CreateMember();
            var apiMember = new UserTests().CreateAPIMember();
            var app = new ApplicationTests().CreateApplication(member);
            apiMember.DownloadApplication(app);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    new ApplicationUpdateReviewCommandHandler().Execute(session, new AppReadyGo.Core.Commands.API.ApplicationUpdateReviewCommand(apiMember.Id, app.Id, "Some review"));
                    dbTrans.Commit();
                }
            }
            Assert.AreEqual("Some review", apiMember.DownloadedApplications.First().Review);
        }
    }
}
