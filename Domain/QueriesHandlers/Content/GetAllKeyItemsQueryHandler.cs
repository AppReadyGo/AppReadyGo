using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.QueryResults.Admin;
using AppReadyGo.Core.QueryResults.Content;

namespace AppReadyGo.Domain.Queries
{
    public class GetAllKeyItemsQueryHandler : IQueryHandler<GetAllKeyItemsQuery, GetAllKeyItemsQueryResult>
    {
        public GetAllKeyItemsQueryResult Run(ISession session, GetAllKeyItemsQuery query)
        {

            var keyQuery = session.Query<Key>()
                                .Where(k => k.Id == query.Id);

            var res = new GetAllKeyItemsQueryResult 
            {
                KeyUrl = keyQuery.Select(k => k.Url).Single()
            };

            var itemsQuery = keyQuery.SelectMany(k => k.Items);

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                itemsQuery = itemsQuery.Where(i => i.SubKey.Contains(query.SearchStr));
            }

            res.Count = itemsQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;

            var keys = itemsQuery.Select(i => new ItemResult
            {
                Id = i.Id,
                SubKey = i.SubKey,
                IsHTML = i.IsHTML,
                Value = i.Value
            });

            if (query.OrderBy == GetAllKeyItemsQuery.OrderByColumn.Id)
            {
                keys = query.ASC ? keys.OrderBy(u => u.Id) : keys.OrderByDescending(u => u.Id);
            }
            else if (query.OrderBy == GetAllKeyItemsQuery.OrderByColumn.SubKey)
            {
                keys = query.ASC ? keys.OrderBy(u => u.SubKey) : keys.OrderByDescending(u => u.SubKey);
            }
            else if (query.OrderBy == GetAllKeyItemsQuery.OrderByColumn.IsHtml)
            {
                keys = query.ASC ? keys.OrderBy(u => u.IsHTML) : keys.OrderByDescending(u => u.IsHTML);
            }

            res.Items = keys.Skip(res.PageSize * (res.CurPage - 1))
                        .Take(res.PageSize)
                        .ToArray();

            return res;
        }
    }
}
