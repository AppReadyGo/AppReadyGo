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

namespace AppReadyGo.Domain.QueriesHandlers.Tasks
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
                Descriptions = session.Query<Model.TaskDescription>()
                                .Select(d => d)
                                .ToDictionary(k => k.Id, v => v.Description),
            };

            if (query.Id.HasValue)
            {
                result.Task = session.Query<Model.Task>()
                                    .Where(t => t.Application.User.Id == securityContext.CurrentUser.Id && t.Id == query.Id)
                                    .Select(t => new TaskDetailsResult
                                    {
                                        Id = t.Id,
                                        AgeRange = t.AgeRange,
                                        Country = t.Country != null ? new System.Tuple<int,string>(t.Country.GeoId, t.Country.ISOCode) : null,
                                        CreatedDate = t.CreatedDate,
                                        Gender = t.Gender,
                                        Zip = t.Zip,
                                        PublishDate = t.PublishDate,
                                        Description = t.Description.Description,
                                        DescriptionId = t.Description.Id,
                                        Audence = t.Audence,
                                        ApplicationId = t.Application.Id,
                                        ApplicationName = t.Application.Name
                                    })
                                    .Single();
            }

            return result;
        }
    }
}