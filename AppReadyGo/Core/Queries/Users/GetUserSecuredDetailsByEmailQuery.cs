using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Users;

namespace AppReadyGo.Core.Queries.Users
{
    public class GetUserSecuredDetailsByEmailQuery : IQuery<UserSecuredDetailsResult>
    {
        public string Email { get; private set; }

        public UserType[] UserTypes { get; private set; }

        public GetUserSecuredDetailsByEmailQuery(string email, params UserType[] userTypes)
        {
            this.Email = email;
            this.UserTypes = userTypes;
        }
    }
}
