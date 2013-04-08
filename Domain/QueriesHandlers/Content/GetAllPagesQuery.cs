using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetAllPagesQueryHandler : IQueryHandler<GetAllPagesQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        public IEnumerable<KeyValuePair<int, string>> Run(NHibernate.ISession session, GetAllPagesQuery query)
        {
            return session.Query<Page>()
                          .Select(p => new KeyValuePair<int, string>(p.Id, p.Url))
                          .ToArray();
        }
    }
}
