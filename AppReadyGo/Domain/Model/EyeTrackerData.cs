using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Domain.Model
{
    public class EyeTrackerData
    {
        public IEnumerable<ScreenSize> ScreenSizes { get; set; }
        public IEnumerable<string> PageUris { get; set; }
    }
}
