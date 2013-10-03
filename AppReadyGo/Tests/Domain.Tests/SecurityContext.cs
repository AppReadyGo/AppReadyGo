using System.Web;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Core;
using System;

namespace Domain.Tests
{
    public class MockSecurityContext : ISecurityContext
    {
        public CurrentUserDetails CurrentUser { get; private set; }

        public MockSecurityContext(CurrentUserDetails details)
        {
            this.CurrentUser = details;
        }

        public void ClearCurrentUserDetails()
        {
            throw new NotImplementedException();
        }
    }
}
