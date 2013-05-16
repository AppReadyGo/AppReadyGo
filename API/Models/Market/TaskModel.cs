using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    public class TaskModel
    {
        public string Email { get; set; }

        public int TaskId { get; set; }

        public int AppId { get; set; }
    }
}