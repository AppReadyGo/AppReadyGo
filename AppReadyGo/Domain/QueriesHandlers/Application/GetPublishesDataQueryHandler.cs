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
using AppReadyGo.Core.QueryResults.Task;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetPublishesDataQueryHandler : IQueryHandler<GetPublishesDataQuery, PublishesDataResult>
    {
        public PublishesDataResult Run(ISession session, GetPublishesDataQuery query)
        {
            var result = new PublishesDataResult();
            result.PublishesDetails = session.Query<Model.PublishDetails>()
                            .Where(p => p.Application.Id == query.ApplicationId)
                            .Select(p => new TaskDetailsResult
                            {
                                Id = p.Id,
                                AgeRange = p.AgeRange,
                                Gender = p.Gender,
                                //Country = p.Country == null ? null : new System.Tuple<int, string>(p.Country.Code, p.Country.Name),
                                Zip = p.Zip,
                                CreatedDate = p.CreatedDate
                            })
                            .ToArray();

            result.ApplicationName = session.Query<Model.Application>()
                            .Where(a => a.Id == query.ApplicationId)
                            .Select(a => a.Name)
                            .Single();
            return result;
        }
    }
}
