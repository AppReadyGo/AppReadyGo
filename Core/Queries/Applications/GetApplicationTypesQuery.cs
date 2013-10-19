using AppReadyGo.Core.QueryResults.Applications;
using System;
using System.Collections.Generic;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetApplicationTypesQuery : IQuery<IEnumerable<Tuple<int, string>>>
    {
        public GetApplicationTypesQuery()
        {
        }
    }
}
