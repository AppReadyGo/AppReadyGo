using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Applications
{
    public class ApplicationEditDataResult
    {
        public ApplicationDetailsResult ApplicationDetails { get; set; }

        public IEnumerable<Tuple<int, string>> ApplicationTypes { get; set; }

        public IEnumerable<Tuple<int, string>> Screenshots { get; set; }
    }
}
