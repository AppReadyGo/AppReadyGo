using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.QueryResults.Tasks;

namespace AppReadyGo.Core.Queries.Tasks
{
    public class GetTaskDataQuery : IQuery<TaskDataResult>
    {
        public int? Id { get; set; }

        public GetTaskDataQuery(int? id = null)
        {
            this.Id = id;
        }
    }
}
