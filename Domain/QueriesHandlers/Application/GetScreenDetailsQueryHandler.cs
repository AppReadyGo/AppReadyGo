using System.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Applications;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetScreenDetailsQueryHandler : IQueryHandler<GetScreenDetailsQuery, ScreenDetailsResult>
    {
        public ScreenDetailsResult Run(ISession session, GetScreenDetailsQuery query)
        {
            return session.Query<Model.Screen>()
                    .Where(s => s.Id == query.Id)
                    .Select(s => new ScreenDetailsResult
                    {
                        Id = s.Id,
                        Path = s.Path,
                        Width = s.Width,
                        Height = s.Height,
                        FileExtention = s.FileExtension,
                        ApplicationId = s.Application.Id,
                    })
                    .SingleOrDefault();
        }
    }
}
