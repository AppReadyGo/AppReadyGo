using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Analytics;

namespace AppReadyGo.Core.Queries.Analytics
{
    public class AnalyticsScreenDataQuery : IQuery<AnalyticsScreenDataResult>
    {
        public int TaskId { get; private set; }

        public int ScreenId { get; private set; }

        public AnalyticsScreenDataQuery(int taskId, int screenId)
        {
            this.TaskId = taskId;
            this.ScreenId = screenId;
        }
    }
}
