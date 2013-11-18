using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.QueryResults.Analytics;

namespace AppReadyGo.Core.Queries.Analytics
{
    public class TaskDashboardDataQuery : IQuery<TaskDashboardDataResult>
    {
        public int TaskId { get; private set; }

        public TaskDashboardDataQuery(int taskId)
        {
            this.TaskId = taskId;
        }
    }
}
