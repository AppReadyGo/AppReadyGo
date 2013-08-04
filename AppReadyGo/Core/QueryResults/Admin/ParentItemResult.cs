using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class ParentItemResult : ItemResult
    {
        public int ParentId { get; set; }

        public string ParentUrl { get; set; }
    }
}
