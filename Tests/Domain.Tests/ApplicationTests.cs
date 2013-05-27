using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Domain.CommandHandlers;
using AppReadyGo.Common;
using NHibernate;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Domain.Model;

namespace Domain.Tests
{
    [TestClass]
    public class ApplicationTests
    {
        private Database database;

        [TestInitialize()]
        public void TestInitialize()
        {
            this.database = new Database("QA", "arg_unittests");
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            this.database.Clear();
        }

        internal static Application CreateApplication(Database database, Member member, string name = "Some name")
        {
            var appType = new ApplicationType("Some type");
            var app = new Application(member, name, "Some description", appType, null);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    session.Save(appType);
                    session.Save(app);
                    dbTrans.Commit();
                }
            }
            return app;
        }
    }
}
