using System.Collections.Generic;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class AllKeysResult : PageingResult
    {
        public IEnumerable<KeyDetailsResult> Keys { get; set; }
    }
}
