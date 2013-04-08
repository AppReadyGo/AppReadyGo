using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class ApplicationsDataResult : PageingResult
    {
        public IEnumerable<ApplicationDataItemResult> Applications { get; set; }

        public IEnumerable<ApplicationResult> TopApplications { get; set; }

        public IEnumerable<ApplicationScreenResult> TopScreens { get; set; }
    }
}
