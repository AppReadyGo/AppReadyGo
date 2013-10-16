using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.QueryResults.Task;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class ApplicationIndexData
    {
        public TaskDetailsResult[] Tasks { get; set; }

        public ApplicationResult[] Applications { get; set; }
    }
}
