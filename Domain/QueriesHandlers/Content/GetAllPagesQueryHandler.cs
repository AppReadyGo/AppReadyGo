using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.QueryResults.Admin;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetAllPagesQueryHandler : IQueryHandler<GetAllPagesQuery, AllPagesResult>
    {
        public AllPagesResult Run(NHibernate.ISession session, GetAllPagesQuery query)
        {
            var res = new AllPagesResult();

            var pagesQuery = session.Query<Page>();

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                pagesQuery = pagesQuery.Where(k => k.Url.Contains(query.SearchStr));
            }

            res.Count = pagesQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;

            var pages = pagesQuery.Select(k => new PageDetailsResult
            {
                Id = k.Id,
                Url = k.Url,
                Theme = k.Theme.Name,
                ItemsCount = k.Items.Count()
            });

            if (query.OrderBy == GetAllPagesQuery.OrderByColumn.Id)
            {
                pages = query.ASC ? pages.OrderBy(u => u.Id) : pages.OrderByDescending(u => u.Id);
            }
            else if (query.OrderBy == GetAllPagesQuery.OrderByColumn.Url)
            {
                pages = query.ASC ? pages.OrderBy(u => u.Url) : pages.OrderByDescending(u => u.Url);
            }
            else if (query.OrderBy == GetAllPagesQuery.OrderByColumn.Theme)
            {
                pages = query.ASC ? pages.OrderBy(u => u.Theme) : pages.OrderByDescending(u => u.Theme);
            }

            res.Pages = pages.Skip(res.PageSize * (res.CurPage - 1))
                        .Take(res.PageSize)
                        .ToArray();

            return res;
        }
    }
}
