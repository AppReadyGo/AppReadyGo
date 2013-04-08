using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetAllThemesQueryHandler : IQueryHandler<GetAllThemesQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        public IEnumerable<KeyValuePair<int, string>> Run(ISession session, GetAllThemesQuery query)
        {
            return session.Query<Theme>()
                          .Select(t => new KeyValuePair<int, string>(t.Id, t.Name))
                          .ToArray();
        }
    }
}
