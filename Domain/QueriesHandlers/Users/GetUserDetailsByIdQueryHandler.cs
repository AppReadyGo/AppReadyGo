﻿using System.Collections.Generic;
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
    public class GetUserDetailsByIdQueryHandler : IQueryHandler<GetUserDetailsByIdQuery, UserDetailsResult>
    {
        public UserDetailsResult Run(ISession session, GetUserDetailsByIdQuery query)
        {
            return session.Query<User>()
                    .Where(u => u.Id == query.Id)
                    .Select(u => new UserDetailsResult
                    {
                        Type = u.Type,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    })
                    .SingleOrDefault();
        }
    }
}
