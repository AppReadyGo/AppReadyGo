using System.Collections.Generic;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetAllApplicationsQuery : IQuery<IEnumerable<ApplicationResult>>
    {
        public GetAllApplicationsQuery()
        {
        }
    }
}
