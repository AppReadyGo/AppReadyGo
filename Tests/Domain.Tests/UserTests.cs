using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Domain.CommandHandlers;
using AppReadyGo.Common;
using NHibernate;
using AppReadyGo.Domain.Model.Users;

namespace Domain.Tests
{
    [TestClass]
    public class UserTests
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

        internal static Member CreateMember(Database database, string email = "member@test.com")
        {
            var user = new Member(email, "12345");
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    session.Save(user);
                    dbTrans.Commit();
                }
            }
            return user;
        }

        internal static ApiMember CreateAPIMember(Database database, string email = "APIMember@test.com")
        {
            var user = new ApiMember(email, "12345", "Some name", "some name", null, null, null, null, new AppReadyGo.Domain.Model.ApplicationType[0]);
            using (ISession session = database.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    session.Save(user);
                    dbTrans.Commit();
                }
            }
            return user;
        }
    }
}
