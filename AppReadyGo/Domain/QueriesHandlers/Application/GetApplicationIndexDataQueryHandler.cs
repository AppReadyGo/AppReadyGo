using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Core.QueryResults.Tasks;
using AppReadyGo.Domain.Model;
using AppReadyGo.Domain.Queries;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.QueriesHandlers.Application
{
    public class GetApplicationIndexDataQueryHandler : IQueryHandler<GetApplicationIndexDataQuery, ApplicationIndexData>
    {
        private ISecurityContext securityContext;

        public GetApplicationIndexDataQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public ApplicationIndexData Run(ISession session, GetApplicationIndexDataQuery query)
        {
            var result = new ApplicationIndexData { };
            result.Tasks = session.Query<AppReadyGo.Domain.Model.Application>()
                                .Where(a => a.User.Id == this.securityContext.CurrentUser.Id)
                                .SelectMany(a => a.Tasks)
                                .Select(p => new TaskDetailsResult
                                {
                                    Id = p.Id,
                                    AgeRange = p.AgeRange,
                                    Country = p.Country != null ? new System.Tuple<int,string>(p.Country.GeoId, p.Country.ISOCode) : null,
                                    CreatedDate = p.CreatedDate,
                                    Gender = p.Gender,
                                    Zip = p.Zip,
                                    PublishDate = p.PublishDate,
                                    Description = p.Description.Description,
                                    Audence = p.Audence,
                                    ApplicationId = p.Application.Id,
                                    ApplicationName = p.Application.Name
                                })
                                .ToArray();

            var appIds = result.Tasks.Select(t => t.ApplicationId).ToArray();

            var installed = session.Query<AppReadyGo.Domain.Model.Users.ApiMember>()
                                        .SelectMany(u => u.Tasks)
                                        .Where(t => appIds.Contains(t.Task.Application.Id))
                                        .GroupBy(t => t.Task.Id)
                                        .Select(g => new { id = g.Key, Count = g.Count() })
                                        .ToDictionary(k => k.id, v => v.Count);

            foreach (var t in result.Tasks)
            {
                t.Installs = installed.ContainsKey(t.Id) ? installed[t.Id] : 0;
            }

            result.Applications = session.Query<AppReadyGo.Domain.Model.Application>()
                                .Where(a => a.User.Id == this.securityContext.CurrentUser.Id)
                                .Select(p => new ApplicationResult
                                {
                                    Id = p.Id,
                                    Name = p.Name
                                })
                                .ToArray();
            return result;
        }
    }
}