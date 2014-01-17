using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.Queries
{
    public class GetUserDetailsByEmailQueryHandler : IQueryHandler<GetUserDetailsByEmailQuery, UserDetailsResult>
    {
        public UserDetailsResult Run(ISession session, GetUserDetailsByEmailQuery query)
        {
            return session.Query<User>()
                    .Where(u => u.Email.ToLower() == query.Email.ToLower() && query.Type.Contains(u.Type))
                    .Select(u => new UserDetailsResult
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    })
                    .SingleOrDefault();
        }
    }
}
