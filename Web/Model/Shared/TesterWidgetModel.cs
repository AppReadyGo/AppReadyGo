using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Shared
{
    public class TesterWidgetModel
    {
        public int Count { get; set; }

        public IEnumerable<string> LastTesters { get; set; }
    }
}