using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Results.Admin
{
    public class LogDataResult
    {
        public IEnumerable<LogResult> Log { get; set; }

        public IDictionary<int, string> Categories { get; set; }

        public IEnumerable<string> Severities { get; set; }
    }
}
