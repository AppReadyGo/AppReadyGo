using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Users
{
    public class StaffFullDetailsResult : UserFullDetailsResult
    {
        public IEnumerable<StaffRole> Roles { get; set; }
    }
}
