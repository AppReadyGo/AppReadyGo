using System.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetApplicationEditDataQueryHandler : IQueryHandler<GetApplicationEditDataQuery, ApplicationEditDataResult>
    {
        public ApplicationEditDataResult Run(ISession session, GetApplicationEditDataQuery query)
        {
            var result = new ApplicationEditDataResult();
            result.ApplicationDetails = session.Query<Model.Application>()
                        .Where(a => a.Id == query.Id)
                        .Select(a => new ApplicationDetailsResult
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            Type = new System.Tuple<int,string>(a.Type.Id, a.Type.Name),
                            IconExt = a.IconExt,
                            PackageFileName = a.PackageFileName
                        })
                        .SingleOrDefault();

            result.ApplicationTypes = session.Query<ApplicationType>()
                        .Select(x => new System.Tuple<int, string>(x.Id, x.Name))
                        .ToArray();

            result.Screenshots = session.Query<Model.Application>()
                        .Where(a => a.Id == query.Id)
                        .SelectMany(a => a.Screenshots)
                        .Select(s => new System.Tuple<int, string>(s.Id, s.FileExtension))
                        .ToArray();

            return result;
        }
    }
}
