using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Core.Queries;
using AppReadyGo.Core.QueryResults;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetApplicationDetailsQuery : IQuery<ApplicationDetailsResult>
    {
        public int Id { get; private set; }

        public GetApplicationDetailsQuery(int id)
        {
            this.Id = id;
        }
    }
}
