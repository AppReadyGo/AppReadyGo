using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults
{
    public class PageingResult
    {
        public int CurPage { get; set; }

        public int TotalPages { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }
    }
}
