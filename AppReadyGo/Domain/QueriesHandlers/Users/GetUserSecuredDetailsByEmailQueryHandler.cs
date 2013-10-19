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
    public class GetUserSecuredDetailsByEmailQueryHandler : IQueryHandler<GetUserSecuredDetailsByEmailQuery, UserSecuredDetailsResult>
    {
        public UserSecuredDetailsResult Run(ISession session, GetUserSecuredDetailsByEmailQuery query)
        {
            var user = session.Query<User>()
                    .Where(u => u.Email.ToLower() == query.Email.ToLower() && query.UserTypes.Contains(u.Type))
                    .Select(u => new
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Password = u.Password,
                        PasswordSalt = u.PasswordSalt,
                        Activated = u.Activated,
                        Type = u.Type,
                        SpecialAccess = u.SpecialAccess,
                        AcceptedTermsAndConditions = u.AcceptedTermsAndConditions
                    })
                    .SingleOrDefault();
            if (user != null)
            {
                return new UserSecuredDetailsResult
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    PasswordSalt = user.PasswordSalt,
                    Type = user.Type,
                    Activated = user.Activated,
                    SpecialAccess = user.SpecialAccess,
                    AcceptedTermsAndConditions = user.AcceptedTermsAndConditions,
                    Roles = user.Type == AppReadyGo.Core.UserType.Staff ? session.Query<Staff>()
                                                                                    .Where(u => u.Id == user.Id)
                                                                                    .SelectMany(u => u.Roles)
                                                                                    .Select(r => r)
                                                                                    .ToArray() : null
                };
            }
            else
            {
                return null;
            }
        }
    }
}
