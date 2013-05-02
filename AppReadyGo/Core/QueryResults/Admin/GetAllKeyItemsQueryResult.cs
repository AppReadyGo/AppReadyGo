using AppReadyGo.Core.QueryResults.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class GetAllKeyItemsQueryResult : PageingResult
    {
        public IEnumerable<ItemResult> Items { get; set; }

        public string KeyUrl { get; set; }
    }
}
