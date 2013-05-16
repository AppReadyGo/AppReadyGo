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
    public class GetApplicationsForUserQueryHandler : IQueryHandler<GetApplicationsForUserQuery, IEnumerable<APIApplicationResult>>
    {
        public IEnumerable<APIApplicationResult> Run(ISession session, GetApplicationsForUserQuery query)
        {
            // TODO: Change application filtering by user details
            return session.Query<AppReadyGo.Domain.Model.Application>()
                    .Select(a => new APIApplicationResult
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        HasIcon = a.IconExt != null
                    })
                    .ToArray();

            // TODO: add paging
            //var apps = session.Query<AppReadyGo.Domain.Model.Application>();
            //int count = 

            //var skip = query.PageSize * (query.CurPage - 1);
            //result.Transactions = allTransactions.Skip(skip)
            //            .Take(query.PageSize)
            //            .ToArray();

            //        .Select(a => new APIApplicationResult
            //        {
            //            Id = a.Id,
            //            Name = a.Name,
            //            Description = a.Description,
            //            HasIcon = a.IconExt != null
            //        })
            //        .ToArray();
        }
    }
}
