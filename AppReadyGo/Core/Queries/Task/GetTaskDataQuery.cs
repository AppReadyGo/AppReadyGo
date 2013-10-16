using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.QueryResults.Task;

namespace AppReadyGo.Core.Queries.Task
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
