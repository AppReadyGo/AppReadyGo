using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppReadyGo.Domain.CommandHandlers;
using AppReadyGo.Common;
using NHibernate;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Core;

namespace Domain.Tests
{
    [TestClass]
    public class ValidationContextTest
    {
        private Database database;

        [TestInitialize()]
        public void TestInitialize()
        {
            this.database = new Database(@"QA", "arg_unittests");
        }

        [TestCleanup()]
        public void TestCleanUp()
        {
            this.database.Clear();
        }

        [TestMethod]
        public void IsEmailExistsForNewUser()
        {
            var member = UserTests.CreateMember(database);
            var details = new CurrentUserDetails(member.Id, member.Type, member.Email, member.Email);
            using (ISession session = database.OpenSession())
            {
                var validation = new ValidationContext(session, new MockSecurityContext(details));
                Assert.IsTrue(validation.IsEmailExists("test@test.com"));
                Assert.IsFalse(validation.IsEmailExists("test1@test.com"));
            }
        }

        [TestMethod]
        public void IsEmailExistsForExistsUser()
        {
            var member = UserTests.CreateMember(database, "test1@test.com");
            int userId = UserTests.CreateMember(database).Id;
            var details = new CurrentUserDetails(member.Id, member.Type, member.Email, member.Email);
            using (ISession session = database.OpenSession())
            {
                var validation = new ValidationContext(session, new MockSecurityContext(details));
                Assert.IsFalse(validation.IsEmailExists("test@test.com", userId));
                Assert.IsTrue(validation.IsEmailExists("test1@test.com", userId));
            }
        }
    }
}
