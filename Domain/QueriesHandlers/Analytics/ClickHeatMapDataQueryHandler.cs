using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Core;
using AppReadyGo.Domain.Model;

namespace AppReadyGo.Domain.Queries
{
    public class ClickHeatMapDataQueryHandler : IQueryHandler<ClickHeatMapDataQuery, ClickHeatMapDataResult>
    {
        private ISecurityContext securityContext;

        public ClickHeatMapDataQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public ClickHeatMapDataResult Run(ISession session, ClickHeatMapDataQuery query)
        {
            var result = new ClickHeatMapDataResult();
            string path = query.Path;

            int appId = session.Query<Task>()
                            .Where(t => t.Id == query.TaskId)
                            .Select(t => t.Application.Id)
                            .Single();

            if (query.ScreenId.HasValue)
            {
                result.Screen = session.Query<Screen>()
                                    .Where(s => s.Id == query.ScreenId.Value)
                                    .Select(s => new ScreenResult
                                    {
                                        Id = s.Id,
                                        Path = s.Path,
                                        ApplicationId = s.Application.Id,
                                        Height = s.Height,
                                        Width = s.Width,
                                        FileExtension = s.FileExtension
                                    })
                                    .FirstOrDefault();

                result.ScreenSize = result.Screen.Size;
                path = result.Screen.Path;
            }
            else
            {
                result.ScreenSize = new System.Drawing.Size(query.Width.Value, query.Height.Value);
            }

            result.Data = session.Query<PageView>()
                    .Where(p => p.Application.Id == appId &&
                                p.Path.ToLower() == path.ToLower() &&
                                p.ScreenWidth == result.ScreenSize.Width &&
                                p.ScreenHeight == result.ScreenSize.Height)
                    .SelectMany(p => p.Clicks)
                    .GroupBy(c => new { X = c.X, Y = c.Y })
                    .Select(c => new ClickHeatMapItemResult { ClientX = c.Key.X, ClientY = c.Key.Y, Count = c.Count() })
                    .ToArray();

            return result;
        }
    }
}
