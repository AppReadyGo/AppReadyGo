using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.QueryResults.Task
{
    public class TaskDataResult
    {
        public TaskDetailsResult Task { get; set; }

        public IEnumerable<ApplicationResult> Applications { get; set; }

        public IDictionary<int, string> Countries { get; set; }
    }
}
