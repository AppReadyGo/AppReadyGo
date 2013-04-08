using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Core.Queries;
using AppReadyGo.Core.QueryResults;

namespace AppReadyGo.Core.Queries.Users
{
    public class GetPortfolioDetailsQuery : IQuery<PortfolioDetailsResult>
    {
        public int Id { get; private set; }

        public GetPortfolioDetailsQuery(int id)
        {
            this.Id = id;
        }
    }
}
