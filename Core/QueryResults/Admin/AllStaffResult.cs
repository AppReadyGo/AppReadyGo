﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Users;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class AllStaffResult : PageingResult
    {
        public IEnumerable<StaffFullDetailsResult> Users { get; set; }
    }
}
