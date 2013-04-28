using System.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;
using System.Collections.Generic;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetPublishDetailsQueryHandler : IQueryHandler<GetPublishDetailsQuery, IEnumerable<PublishDetailsResult>>
    {
        public IEnumerable<PublishDetailsResult> Run(ISession session, GetPublishDetailsQuery query)
        {
            return session.Query<Model.PublishDetails>()
                        .Where(p => p.Id == query.Id)
                        .Select(p => new PublishDetailsResult
                        {
                            Id = p.Id,
                            AgeRange = p.AgeRange,
                            Gender = p.Gender,
                            Country = p.Country == null ? null : new System.Tuple<int, string>(p.Country.Code, p.Country.Name),
                            Zip = p.Zip,
                            CreatedDate = p.CreatedDate
                        })
                        .ToArray();
        }
    }
}
