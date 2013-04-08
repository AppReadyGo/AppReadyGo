using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Core.QueryResults.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetThemeQueryHandler : IQueryHandler<GetThemeQuery, ThemeResult>
    {
        public ThemeResult Run(NHibernate.ISession session, GetThemeQuery query)
        {
            return session.Query<Theme>()
                            .Where(t => t.Url.ToLower() == query.Url.ToLower())
                            .Select(t => new ThemeResult
                            {
                                Id = t.Id,
                                Url = t.Url,
                                Name = t.Name,
                                Type = t.Type
                            })
                            .SingleOrDefault();
        }
    }
}
