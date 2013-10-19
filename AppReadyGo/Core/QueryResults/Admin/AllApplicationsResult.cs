using System.Collections.Generic;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Core.QueryResults.Users;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class AllApplicationsResult : PageingResult
    {
        public IEnumerable<ApplicationFullDetailsResult> Applications { get; set; }
    }
}
