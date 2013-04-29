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
    public class GetPublishesDataQuery : IQuery<PublishesDataResult>
    {
        public int ApplicationId { get; private set; }

        public GetPublishesDataQuery(int applicationId)
        {
            this.ApplicationId = applicationId;
        }
    }
}
