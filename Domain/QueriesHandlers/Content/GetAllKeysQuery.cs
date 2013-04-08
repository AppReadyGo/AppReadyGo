using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries.Content;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries
{
    public class GetAllKeysQueryHandler : IQueryHandler<GetAllKeysQuery, IEnumerable<KeyValuePair<int, string>>>
    {
        public IEnumerable<KeyValuePair<int, string>> Run(ISession session, GetAllKeysQuery query)
        {
            return session.Query<Key>()
                          .Select(k => new KeyValuePair<int, string>(k.Id, k.Url))
                          .ToArray();
        }
    }
}
