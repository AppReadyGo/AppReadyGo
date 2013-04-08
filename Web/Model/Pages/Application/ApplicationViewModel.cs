using AppReadyGo.Core.QueryResults.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.Model.Pages.Application
{
    public class ApplicationViewModel
    {
        public List<ScreenResult> Screens { get; set; }
        public IEnumerable<SelectListItem> TypesList { get; set; }
        public string PropertyId { get; set; }
    }
}