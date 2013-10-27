using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    public class TaskModel
    {
        public int UserId { get; set; }

        public int TaskId { get; set; }
    }

    public class ReviewModel : TaskModel
    {
        public string Review { get; set; }

        public byte Rate { get; set; }
    }

    public class DownloadedModel : TaskModel
    {
        public bool Downloaded { get; set; }
    }

}