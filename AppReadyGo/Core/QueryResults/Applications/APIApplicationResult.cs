using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Applications
{
    public class APIApplicationResult : ApplicationResult
    {
        public string Description { get; set; }

        public bool HasIcon { get; set; }

        public string Url { get; set; }

        public Dictionary<int, string> Screenshots { get; set; }
    }
}
