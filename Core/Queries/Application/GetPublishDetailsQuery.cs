using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Core.Queries;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetPublishDetailsQuery : IQuery<IEnumerable<PublishDetailsResult>>
    {
        public int Id { get; private set; }

        public GetPublishDetailsQuery(int id)
        {
            this.Id = id;
        }
    }
}
