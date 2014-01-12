using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Analytics
{
    public class HeatMapDataResult
    {
        public Size ScreenSize { get; set; }

        public ScreenResult Screen { get; set; }

        public IEnumerable<HeatMapItemResult> Data { get; set; }
    }
}
