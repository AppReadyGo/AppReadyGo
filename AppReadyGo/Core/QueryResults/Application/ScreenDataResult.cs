using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class ScreenDataResult
    {
        public int ApplicationId { get; set; }

        public string ApplicationDescription { get; set; }

        public IEnumerable<string> Pathes { get; set; }

        public IEnumerable<Size> Sizes { get; set; }
    }
}
