using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    public class TaskModel
    {
        public int UserId { get; set; }

        public int AppId { get; set; }
    }

    public class ReviewModel : TaskModel
    {
        public string Review { get; set; }
    }
}