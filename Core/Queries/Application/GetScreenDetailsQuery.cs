using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetScreenDetailsQuery : IQuery<ScreenDetailsResult>
    {
        public int Id { get; private set; }

        public GetScreenDetailsQuery(int id)
        {
            this.Id = id;
        }
    }
}
