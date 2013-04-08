using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Core;

namespace AppReadyGo.Domain.Queries
{
    public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, IEnumerable<StaffRole>>
    {
        public IEnumerable<StaffRole> Run(ISession session, GetUserRolesQuery query)
        {
            return session.Query<Staff>()
                    .Where(u => u.Id == query.Id)
                    .SelectMany(u => u.Roles)
                    .Select(r => r)
                    .ToArray();
        }
    }
}
