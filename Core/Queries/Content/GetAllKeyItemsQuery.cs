using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.QueryResults.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Queries.Content
{
    public class GetAllKeyItemsQuery : GetAllItemsQuery, IQuery<GetAllKeyItemsQueryResult>
    {
        public GetAllKeyItemsQuery(int keyId, string searchStr, OrderByColumn orderBy, bool asc, int curPage, int pageSize)
            : base(keyId, searchStr, orderBy, asc, curPage, pageSize)
        {
        }
    }
}
