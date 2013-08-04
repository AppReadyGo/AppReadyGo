using System.Collections.Generic;
using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class GetAllKeyItemsQueryResult : PageingResult
    {
        public IEnumerable<ItemResult> Items { get; set; }

        public string KeyUrl { get; set; }
    }
}
