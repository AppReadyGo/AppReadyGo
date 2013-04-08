using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Users;

namespace AppReadyGo.Core.Queries.Users
{
    public class GetUserDetailsByIdQuery : IQuery<UserDetailsResult>
    {
        public int Id { get; private set; }

        public GetUserDetailsByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
