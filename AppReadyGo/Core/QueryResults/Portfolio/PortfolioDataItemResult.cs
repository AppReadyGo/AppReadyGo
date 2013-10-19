using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Applications;

namespace AppReadyGo.Core.QueryResults.Portfolio
{
    public class PortfolioDataItemResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public long Visits { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<ApplicationDataItemResult> Applications { get; set; }
    }
}
