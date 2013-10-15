using System;
using System.Linq;
using System.Reflection;
using AppReadyGo.Core;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;
using AppReadyGo.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Drawing;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, IEnumerable<ApplicationResult>>
    {
        private ISecurityContext securityContext;

        public GetAllApplicationsQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public IEnumerable<ApplicationResult> Run(ISession session, GetAllApplicationsQuery query)
        {
            return session.Query<Model.Application>()
                        .Where(a => a.User.Id == securityContext.CurrentUser.Id)
                        .Select(a => new ApplicationResult
                                            {
                                                Id = a.Id,
                                                Name = a.Name
                                            })
                                            .ToArray();
        }
    }
}
