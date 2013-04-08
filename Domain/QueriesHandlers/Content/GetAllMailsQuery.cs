using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetAllMailsQueryHandler : IQueryHandler<GetAllMailsQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        public IEnumerable<KeyValuePair<int, string>> Run(ISession session, GetAllMailsQuery query)
        {
            return session.Query<Mail>()
                          .Select(m => new KeyValuePair<int, string>(m.Id, m.Url))
                          .ToArray();
        }
    }
}
