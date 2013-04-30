﻿using System.Collections.Generic;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.QueryResults.Portfolio
{
    public class PortfolioResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public IEnumerable<ExtendedApplicationResult> Applications { get; set; }

        public long Visits { get; set; }
    }
}