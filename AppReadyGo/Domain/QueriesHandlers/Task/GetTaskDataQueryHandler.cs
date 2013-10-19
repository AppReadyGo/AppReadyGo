using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Tasks;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Core.QueryResults.Tasks;
using AppReadyGo.Domain.Queries;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.QueriesHandlers.Task
{
    public class GetTaskDataQueryHandler : IQueryHandler<GetTaskDataQuery, TaskDataResult>
    {
        private ISecurityContext securityContext;

        public GetTaskDataQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public TaskDataResult Run(ISession session, GetTaskDataQuery query)
        {
            var result = new TaskDataResult
            {
                Applications =  session.Query<Model.Application>()
                        .Where(a => a.User.Id == securityContext.CurrentUser.Id)
                        .Select(a => new ApplicationResult
                                            {
                                                Id = a.Id,
                                                Name = a.Name
                                            })
                                            .ToArray(),
                Countries = session.Query<Model.Country>()
                                .Select(c => c)
                                .ToDictionary(k => k.GeoId, v => v.Name),
            };

            if (query.Id.HasValue)
            {
                //result.Task =  session.Query<Model>()
                //        .Where(a => a.User.Id == securityContext.CurrentUser.Id)
            }

            return result;
        }
    }
}
