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
using System;
using NHibernate.Transform;

namespace AppReadyGo.Domain.Queries
{
    public class HeatMapDataQueryHandler : IQueryHandler<HeatMapDataQuery, HeatMapDataResult>
    {
        private ISecurityContext securityContext;

        public HeatMapDataQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public HeatMapDataResult Run(ISession session, HeatMapDataQuery query)
        {
            var result = new HeatMapDataResult();
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


            ViewPart viewPart = null;
            ViewPartData viewPartData = null;
            result.Data = session.QueryOver<PageView>()
                .JoinAlias(p => p.ViewParts, () => viewPart)
                .Where(p => p.Application.Id == appId &&
                            p.Path == path &&
                            p.ScreenWidth == result.ScreenSize.Width &&
                            p.ScreenHeight == result.ScreenSize.Height)
                .SelectList(list => list
                .Select(p => p.ScreenWidth).WithAlias(() => viewPartData.ScreenWidth)
                .Select(p => p.ScreenHeight).WithAlias(() => viewPartData.ScreenHeight)
                .Select(p => viewPart.X).WithAlias(() => viewPartData.X)
                .Select(p => viewPart.Y).WithAlias(() => viewPartData.Y)
                .Select(p => viewPart.StartDate).WithAlias(() => viewPartData.StartDate)
                .Select(p => viewPart.FinishDate).WithAlias(() => viewPartData.FinishDate))
                .TransformUsing(Transformers.AliasToBean<ViewPartData>())
                .List<ViewPartData>()
                .GroupBy(d => new { d.X, d.Y, d.ScreenHeight, d.ScreenWidth })
                .Select(g => new HeatMapItemResult { ScrollLeft = g.Key.X, ScrollTop = g.Key.Y, ScreenHeight = g.Key.ScreenHeight, ScreenWidth = g.Key.ScreenWidth, TimeSpan = g.Count() })
                .ToList();

            //var result = session.Query<PageView>()
            //                .Where(p => p.Application.Id == query.AplicationId &&
            //                        p.Path == query.Path &&
            //                        p.ClientWidth == query.ClientWidth &&
            //                        p.ClientHeight == query.ClientHeight &&
            //                        p.Date >= query.FromDate &&
            //                        p.Date <= query.ToDate &&
            //                        p.ViewParts.Any())
            //                .Select(p => p)
            //                .ToArray()
            //                .SelectMany(p => p.ViewParts.Select(v => new { p.ScreenWidth, p.ScreenHeight, v.X, v.Y, v.StartDate, v.FinishDate }))
            //                .GroupBy(d => new { d.X, d.Y, d.ScreenHeight, d.ScreenWidth})
            //                .Select(g => new HeatMapDataResult { ScrollLeft = g.Key.X, ScrollTop = g.Key.Y, ScreenHeight = g.Key.ScreenHeight, ScreenWidth = g.Key.ScreenWidth, TimeSpan = g.Count() })
            //                .ToArray();

            return result;
        }

        private class ViewPartData
        {
            public DateTime StartDate { get; set; }
            public DateTime FinishDate { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int ScreenWidth { get; set; }
            public int ScreenHeight { get; set; }
        }
    }
}
