using System.Collections.Generic;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class AllPagesResult : PageingResult
    {
        public IEnumerable<PageDetailsResult> Pages { get; set; }
    }
}
