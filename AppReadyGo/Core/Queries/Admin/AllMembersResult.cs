using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Users;

namespace AppReadyGo.Core.Queries.Admin
{
    public class AllMembersResult : PageingResult
    {
        public IEnumerable<UserFullDetailsResult> Users { get; set; }
    }
}
