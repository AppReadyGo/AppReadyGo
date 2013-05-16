using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class PagingResult<T>
    {
        public IEnumerable<T> Collection { get; set; }

        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public int ItemsCount { get; set; }
    }
}
