using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class ScreensDataResult : PageingResult
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public IEnumerable<ScreenDataItemResult> Screens { get; set; }
    }
}
