using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Domain.Queries
{
    public class GetApplicationsForUserQueryHandler : IQueryHandler<GetApplicationsForUserQuery, PagingResult<APIApplicationResult>>
    {
        public PagingResult<APIApplicationResult> Run(ISession session, GetApplicationsForUserQuery query)
        {
            var res = new PagingResult<APIApplicationResult>
            {
                CurPage = query.CurPage,
                PageSize = query.PageSize
            };

            // TODO: Change application filtering by user details
            var appsQuery = session.Query<AppReadyGo.Domain.Model.Application>();
            res.ItemsCount = appsQuery.Count();

            var skip = query.PageSize * (query.CurPage - 1);
            res.Collection = appsQuery.Select(a => new APIApplicationResult
                        {
                                Id = a.Id,
                                Name = a.Name,
                                Description = a.Description,
                                HasIcon = a.IconExt != null,
                                Url = a.MarketUrl
                        })
                        .Skip(skip)
                        .Take(query.PageSize)
                        .ToArray();

            return res;
        }
    }
}
