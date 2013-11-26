using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Application
{
    public class TaskDetailsModel
    {
        public int Audence { get; set; }

        public string Target { get; set; }

        public string Published { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    }
}