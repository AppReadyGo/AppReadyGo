using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Applications;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetScreenDataQuery : IQuery<ScreenDataResult>
    {
        public int Id { get; private set; }

        public GetScreenDataQuery(int id)
        {
            this.Id = id;
        }
    }
}
