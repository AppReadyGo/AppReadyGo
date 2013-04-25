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

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetApplicationTypesQueryHandler : IQueryHandler<GetApplicationTypesQuery, IEnumerable<System.Tuple<int, string>>>
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private ISecurityContext securityContext;

        public GetApplicationTypesQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public IEnumerable<System.Tuple<int, string>> Run(ISession session, GetApplicationTypesQuery query)
        {
            return session.Query<ApplicationType>()
                        .Select(x => new System.Tuple<int, string>(x.Id, x.Name))
                        .ToArray();
        }
    }
}
