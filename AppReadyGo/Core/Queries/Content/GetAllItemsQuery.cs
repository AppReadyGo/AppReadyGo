using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.QueryResults.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Queries.Content
{
    public abstract class GetAllItemsQuery
    {
        public int Id { get; protected set; }

        public int CurPage { get; protected set; }

        public int PageSize { get; protected set; }

        public string SearchStr { get; protected set; }

        public bool ASC { get; protected set; }

        public OrderByColumn OrderBy { get; protected set; }

        public GetAllItemsQuery(int id, string searchStr, OrderByColumn orderBy, bool asc, int curPage, int pageSize)
        {
            this.Id = id;
            this.ASC = asc;
            this.CurPage = curPage;
            this.PageSize = pageSize;
            this.SearchStr = searchStr;
            this.OrderBy = orderBy;
        }

        public enum OrderByColumn
        {
            Id,
            SubKey,
            IsHtml
        }
    }
}
