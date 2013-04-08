using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Analytics
{
    public class ClickHeatMapDataResult
    {
        public ScreenResult Screen { get; set; }

        public IEnumerable<ClickHeatMapItemResult> Data { get; set; }
    }
}
