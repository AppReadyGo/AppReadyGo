using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Admin;

namespace AppReadyGo.Domain.Queries
{
    public class GetAllKeysQueryHandler : IQueryHandler<GetAllKeysQuery, AllKeysResult>
    {
        public AllKeysResult Run(ISession session, GetAllKeysQuery query)
        {
            var res = new AllKeysResult();

            var keysQuery = session.Query<Key>();

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                keysQuery = keysQuery.Where(k => k.Url.Contains(query.SearchStr));
            }

            res.Count = keysQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;

            var keys = keysQuery.Select(k => new KeyDetailsResult
            {
                Id = k.Id,
                Url = k.Url,
                ItemsCount = k.Items.Count()
            });

            if (query.OrderBy == GetAllKeysQuery.OrderByColumn.Id)
            {
                keys = query.ASC ? keys.OrderBy(u => u.Id) : keys.OrderByDescending(u => u.Id);
            }
            else if (query.OrderBy == GetAllKeysQuery.OrderByColumn.Url)
            {
                keys = query.ASC ? keys.OrderBy(u => u.Url) : keys.OrderByDescending(u => u.Url);
            }

            res.Keys = keys.Skip(res.PageSize * (res.CurPage - 1))
                        .Take(res.PageSize)
                        .ToArray();

            return res;
        }
    }
}
