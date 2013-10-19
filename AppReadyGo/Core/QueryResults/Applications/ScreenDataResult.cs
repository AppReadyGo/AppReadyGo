using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AppReadyGo.Core.QueryResults.Applications
{
    public class ScreenDataResult
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        public IEnumerable<Size> Sizes { get; set; }
    }
}
