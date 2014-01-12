using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Core.QueryResults.Tasks;
using AppReadyGo.Domain.Model;
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
            var res = session.Query<AppReadyGo.Domain.Model.Task>()
                                .Where(t => t.Id == query.TaskId)
                                .Select(t => new AnalyticsScreenDataResult
                                {
                                    TaskInfo = new TaskDetailsResult
                                    {
                                        Id = t.Id,
                                        AgeRange = t.AgeRange,
                                        Country = t.Country != null ? new System.Tuple<int, string>(t.Country.GeoId, t.Country.ISOCode) : null,
                                        CreatedDate = t.CreatedDate,
                                        Gender = t.Gender,
                                        Zip = t.Zip,
                                        PublishDate = t.PublishDate,
                                        Description = t.Description.Description,
                                        Audence = t.Audence,
                                        ApplicationId = t.Application.Id,
                                        ApplicationName = t.Application.Name
                                    },
                                    ApplicationType = t.Application.Type.Name
                                })
                                .Single();

            res.Path = query.Path;

            res.ScreenList = session.Query<Screen>()
                            .Where(s => s.Application.Id == res.TaskInfo.ApplicationId && s.Path.ToLower() == query.Path.ToLower())
                            .Select(s => new ScreenResult
                            {
                                Id = s.Id,
                                ApplicationId = s.Application.Id,
                                FileExtension = s.FileExtension,
                                Height = s.Height,
                                Path = s.Path,
                                Width = s.Width
                            })
                            .ToArray();


            if (query.ScreenId.HasValue)
            {
                var screen = res.ScreenList.Single(x => x.Id == query.ScreenId.Value);
                res.ScreenSize = screen.Size;
            }
            else
            {

                res.ScreenSize = session.Query<PageView>()
                                .Where(pv => pv.Application.Id == res.TaskInfo.ApplicationId)
                                .Select(pv => new System.Drawing.Size(pv.ScreenWidth, pv.ScreenHeight))
                                .First();
            }


            res.Pathes = session.Query<PageView>()
                            .Where(pv => pv.Application.Id == res.TaskInfo.ApplicationId)
                            .Select(pv => pv.Path)
                            .Distinct()
                            .ToArray();

            res.Views = session.Query<PageView>()
                            .Where(p => p.Application.Id == res.TaskInfo.ApplicationId &&
                                p.Path.ToLower() == query.Path.ToLower() &&
                                p.ScreenWidth == res.ScreenSize.Width &&
                                p.ScreenHeight == res.ScreenSize.Height)
                            .Select(p => p)
                            .Count();

            var clicks = session.Query<Click>()
                                .Where(p => p.PageView.Application.Id == res.TaskInfo.ApplicationId &&
                                    p.PageView.Path.ToLower() == query.Path.ToLower() &&
                                    p.PageView.ScreenWidth == res.ScreenSize.Width &&
                                    p.PageView.ScreenHeight == res.ScreenSize.Height)
                                .Select(c => c.PageView.Id)
                                .ToArray();

            res.AvgClicks = clicks.Any() ? clicks.GroupBy(x => x).Average(x => x.Count()) : 0;

            var scrolls = session.Query<Scroll>()
                                .Where(p => p.PageView.Application.Id == res.TaskInfo.ApplicationId &&
                                    p.PageView.Path.ToLower() == query.Path.ToLower() &&
                                    p.PageView.ScreenWidth == res.ScreenSize.Width &&
                                    p.PageView.ScreenHeight == res.ScreenSize.Height)
                                .Select(c => c.PageView.Id)
                                .ToArray();

            res.AvgScrolls = scrolls.Any() ? scrolls.GroupBy(x => x).Average(x => x.Count()) : 0;

            res.Devices = session.Query<PageView>()
                            .Where(p => p.Application.Id == res.TaskInfo.ApplicationId &&
                                p.Path.ToLower() == query.Path.ToLower() &&
                                p.ScreenWidth == res.ScreenSize.Width &&
                                p.ScreenHeight == res.ScreenSize.Height)
                            .Select(p => p.OperationSystem)
                            .Distinct()
                            .Count();

            //res.Downloads = session.Query<ApiMemberTask>().Where(x => x.Task.Id == query.TaskId).Count();

            //res.Pathes = session.Query<PageView>()
            //                .Where(pv => pv.Application.Id == res.TaskInfo.ApplicationId)
            //                .Select(pv => pv.Path)
            //                .Distinct()
            //                .ToArray();


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
            return res;
        }
    }
}