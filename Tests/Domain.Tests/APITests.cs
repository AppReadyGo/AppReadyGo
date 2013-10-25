using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Domain.CommandHandlers;
using AppReadyGo.Common;
using NHibernate;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Domain.CommandHandlers.API;
using AppReadyGo.Domain.Queries.Application;
using AppReadyGo.Core.Queries.Application;
using System.Configuration;

namespace Domain.Tests
{
    [TestClass]
    public class APITests
    {
#if QA
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["QA"].ConnectionString;
#elif DEBUG
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DEV"].ConnectionString;
#else
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["PROD"].ConnectionString;
#endif
        private static readonly bool UseRemoteDatabase = true;
        private IDatabase database;

        [TestInitialize()]
        public void TestInitialize()
        {
            this.database = UseRemoteDatabase ? (IDatabase)new RemoteDatabase(ConnectionString) : (IDatabase)new Database(@"QA", "arg_unittests");
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            this.database.Clear();
        }

        [TestMethod]
        public void ApplicationDownloadedCommand()
        {
            var member = UserTests.CreateMember(this.database);
            var apiMember = UserTests.CreateAPIMember(this.database);
            var app = ApplicationTests.CreateApplication(this.database, member);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    new ApplicationDownloadedCommandHandler().Execute(session, new AppReadyGo.Core.Commands.API.ApplicationDownloadedCommand(apiMember.Id, app.Id));
                    dbTrans.Commit();
                }
                apiMember = session.Get<ApiMember>(apiMember.Id);
                Assert.AreEqual(1, apiMember.Applications.Count());
            }
        }

        [TestMethod]
        public void ApplicationUsedCommand()
        {
            var member = UserTests.CreateMember(this.database);
            var apiMember = UserTests.CreateAPIMember(this.database);
            var app = ApplicationTests.CreateApplication(this.database, member);
            DownloadApplication(this.database, apiMember.Id, app.Id);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    new ApplicationUsedCommandHandler().Execute(session, new AppReadyGo.Core.Commands.API.ApplicationUsedCommand(apiMember.Id, app.Id));
                    dbTrans.Commit();
                }
                apiMember = session.Get<ApiMember>(apiMember.Id);
                //Assert.IsTrue(apiMember.Applications.First().Used);
            }
        }

        [TestMethod]
        public void ApplicationUpdateReviewCommand()
        {
            var member = UserTests.CreateMember(this.database);
            var apiMember = UserTests.CreateAPIMember(this.database);
            var app = ApplicationTests.CreateApplication(this.database, member);
            DownloadApplication(this.database, apiMember.Id, app.Id);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    new ApplicationUpdateReviewCommandHandler().Execute(session, new AppReadyGo.Core.Commands.API.ApplicationUpdateReviewCommand(apiMember.Id, app.Id, "Some review"));
                    dbTrans.Commit();
                }
                apiMember = session.Get<ApiMember>(apiMember.Id);
               // Assert.AreEqual("Some review", apiMember.Applications.First().Review);
            }
        }

        [TestMethod]
        public void GetApplicationDetailsQuery()
        {
            var member = UserTests.CreateMember(this.database);
            var apiMember = UserTests.CreateAPIMember(this.database);
            var app = ApplicationTests.CreateApplication(this.database, member);
            using (ISession session = database.OpenSession())
            {
                var details = new GetApplicationDetailsQueryHandler().Run(session, new GetApplicationDetailsQuery(app.Id));
                Assert.AreEqual(app.Id, details.Id);
            }
        }

        internal static void DownloadApplication(IDatabase database, int memberId, int appId)
        {
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    new ApplicationDownloadedCommandHandler().Execute(session, new AppReadyGo.Core.Commands.API.ApplicationDownloadedCommand(memberId, appId));
                    dbTrans.Commit();
                }
            }
        }
    }
}
