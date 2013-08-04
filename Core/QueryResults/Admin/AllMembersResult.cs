using System.Collections.Generic;
using AppReadyGo.Core.QueryResults.Users;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class AllMembersResult : PageingResult
    {
        public IEnumerable<UserFullDetailsResult> Users { get; set; }
    }
}
