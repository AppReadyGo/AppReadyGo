using AppReadyGo.Core.QueryResults.Admin;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetAllKeysQuery : IQuery<AllKeysResult>
    {
        public int CurPage { get; set; }

        public int PageSize { get; set; }

        public string SearchStr { get; set; }

        public bool ASC { get; set; }

        public OrderByColumn OrderBy { get; set; }
        
        public GetAllKeysQuery(string searchStr, OrderByColumn orderBy, bool asc, int curPage, int pageSize)
        {
            this.ASC = asc;
            this.CurPage = curPage;
            this.PageSize = pageSize;
            this.SearchStr = searchStr;
            this.OrderBy = orderBy;
        }

        public enum OrderByColumn
        {
            Id,
            Url
        }
    }
}
