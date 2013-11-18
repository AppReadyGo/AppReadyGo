using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Pages.Application;

namespace AppReadyGo.Web.Model.Pages.Analytics
{
    public class TaskDashboardModel
    {
        public ApplicationModel ApplicationDetails { get; set; }

        public TaskModel TaskDetails { get; set; }

        public IEnumerable<string> Pathes { get; set; }
    }
}