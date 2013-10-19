using System.Linq;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Domain.Model;
using AppReadyGo.Core.QueryResults;

namespace AppReadyGo.Domain.Queries.Application
{
    public class PublishQueryHandler : IQueryHandler<TaskQuery, PublishResult>
    {
        public PublishResult Run(ISession session, TaskQuery query)
        {
            var res = new PublishResult();

            res.ApplicationName = session.Query<Model.Application>()
                        .Where(a => a.Id == query.ApplicationId)
                        .Select(a => a.Name)
                        .Single();

            res.Countries = session.Query<Model.Country>()
                        .Select(c => new KeyValueResult
                        {
                            Key = c.GeoId,
                            Value = c.Name
                        })
                        .ToArray();

            return res;
        }
    }
}
