using System.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Application;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetApplicationDetailsQueryHandler : IQueryHandler<GetApplicationDetailsQuery, ApplicationDetailsResult>
    {
        public ApplicationDetailsResult Run(ISession session, GetApplicationDetailsQuery query)
        {
            return session.Query<Model.Application>()
                    .Where(a => a.Id == query.Id)
                    .Select(a => new ApplicationDetailsResult
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        Type = new System.Tuple<int,string>(a.Type.Id, a.Type.Name)
                    })
                    .SingleOrDefault();
        }
    }
}
