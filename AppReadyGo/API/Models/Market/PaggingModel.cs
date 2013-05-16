using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    public class PaggingModel
    {
        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public int ItemsCount { get; set; }
    }
}