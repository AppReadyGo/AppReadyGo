using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Domain.Queries;
using AppReadyGo.Model;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.QueriesHandlers.Analytics
{
    public class AnalyticsScreenDataQueryHandler : IQueryHandler<AnalyticsScreenDataQuery, AnalyticsScreenDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public AnalyticsScreenDataQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;  
            this.securityContext = securityContext;
        }

        public AnalyticsScreenDataResult Run(ISession session, AnalyticsScreenDataQuery query)
        {
            //var res = session.Query<ViewPage>()
            //                    .Where(t => t.Id == query.ScreenId)
            //                    .Select(t => new TaskDashboardDataResult
            //                    {
            //                        TaskInfo = new TaskDetailsResult
            //                        {
            //                            Id = t.Id,
            //                            AgeRange = t.AgeRange,
            //                            Country = t.Country != null ? new System.Tuple<int, string>(t.Country.GeoId, t.Country.ISOCode) : null,
            //                            CreatedDate = t.CreatedDate,
            //                            Gender = t.Gender,
            //                            Zip = t.Zip,
            //                            PublishDate = t.PublishDate,
            //                            Description = t.Description.Description,
            //                            Audence = t.Audence,
            //                            ApplicationId = t.Application.Id,
            //                            ApplicationName = t.Application.Name
            //                        },
            //                        ApplicationType = t.Application.Type.Name
            //                    })
            //                    .Single();

            //res.Downloads = session.Query<ApiMemberTask>().Where(x => x.Task.Id == query.TaskId).Count();

            //res.Pathes = session.Query<PageView>()
            //                .Where(pv => pv.Application.Id == res.TaskInfo.ApplicationId)
            //                .Select(pv => pv.Path)
            //                .Distinct()
            //                .ToArray();

            //res.ScreenList = session.Query<Screen>()
            //                .Where(s => s.Application.Id == res.TaskInfo.ApplicationId)
            //                .Select(s => new { s.Id, s.FileExtension })
            //                .ToDictionary(k => k.Id, v => v.FileExtension);

            //res.ClicksGraphData = session.Query<PageView>()
            //                .Where(pv => pv.Application.Id == res.TaskInfo.ApplicationId)
            //                .Select(pv => new { Path = pv.Path, Clicks = pv.Clicks.Count() })
            //                .ToArray()
            //                .GroupBy(x => x.Path)
            //                .ToDictionary(k => k.Key, v => v.Sum(x => x.Clicks));

            //res.ViewsGraphData = session.Query<PageView>()
            //                .Where(pv => pv.Application.Id == res.TaskInfo.ApplicationId)
            //                .Select(pv => pv.Path)
            //                .ToArray()
            //                .GroupBy(x => x)
            //                .ToDictionary(k => k.Key, v => v.Count());

            //res.ScrollsGraphData = session.Query<PageView>()
            //                .Where(pv => pv.Application.Id == res.TaskInfo.ApplicationId)
            //                .Select(pv => new { Path = pv.Path, Scrolls = pv.Scrolls.Count() })
            //                .ToArray()
            //                .GroupBy(x => x.Path)
            //                .ToDictionary(k => k.Key, v => v.Sum(x => x.Scrolls));
            return null;
        }
    }
}