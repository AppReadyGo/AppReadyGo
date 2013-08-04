using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.QueryResults.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetAllPageItemsQuery : GetAllItemsQuery, IQuery<GetAllKeyItemsQueryResult>
    {
        public GetAllPageItemsQuery(int pageId, string searchStr, OrderByColumn orderBy, bool asc, int curPage, int pageSize)
            : base(pageId, searchStr, orderBy, asc, curPage, pageSize)
        {
        }
    }
}
