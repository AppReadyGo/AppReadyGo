using System.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;
using System.Drawing;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetScreenEditDataQueryHandler : IQueryHandler<GetScreenEditDataQuery, ScreenDetailsDataResult>
    {
        public ScreenDetailsDataResult Run(ISession session, GetScreenEditDataQuery query)
        {
            var data = session.Query<Model.Screen>()
                        .Where(s => s.Id == query.Id)
                        .Select(s => new ScreenDetailsDataResult
                        {
                            Id = s.Id,
                            Path = s.Path,
                            Width = s.Width,
                            Height = s.Height,
                            FileExtention = s.FileExtension,
                            ApplicationId = s.Application.Id,
                            ApplicationName = s.Application.Name,
                        })
                        .SingleOrDefault();

            if (data != null)
            {
                data.Pathes = session.Query<Model.PageView>()
                                        .Where(p => p.Application.Id == data.ApplicationId)
                                        .Select(p => p.Path)
                                        .Distinct()
                                        .ToArray();

                data.Sizes = session.Query<Model.PageView>()
                                        .Select(p => new { p.ScreenWidth, p.ScreenHeight })
                                        .ToArray()
                                        .GroupBy(p => new { p.ScreenWidth, p.ScreenHeight })
                                        .Select(g => new Size(g.Key.ScreenWidth, g.Key.ScreenHeight))
                                        .ToArray();
            }

            return data;
        }
    }
}
