using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class PublishResult
    {
        public IEnumerable<KeyValueResult> Countries { get; set; }

        public IEnumerable<KeyValueResult> Types { get; set; }

        public string ApplicationName { get; set; }
    }
}
