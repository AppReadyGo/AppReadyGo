using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;
using AppReadyGo.Domain.Model;
using AppReadyGo.Domain.Queries;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.QueriesHandlers.Application
{
    public class GetApplicationIndexDataQueryHandler : IQueryHandler<GetApplicationIndexDataQuery, ApplicationIndexData>
    {
        private ISecurityContext securityContext;

        public GetApplicationIndexDataQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public ApplicationIndexData Run(ISession session, GetApplicationIndexDataQuery query)
        {
            var result = new ApplicationIndexData { };
            result.Tasks = session.Query<AppReadyGo.Domain.Model.Application>()
                                .Where(a => a.User.Id == this.securityContext.CurrentUser.Id)
                                .SelectMany(a => a.Publishes)
                                .Select(p => new PublishDetailsResult
                                {
                                    Id = p.Id,
                                    AgeRange = p.AgeRange,
                                    Country = p.Country != null ? new System.Tuple<int,string>(p.Country.GeoId, p.Country.Name) : null,
                                    CreatedDate = p.CreatedDate,
                                    Gender = p.Gender,
                                    Zip = p.Zip
                                })
                                .ToArray();

            result.Applications = session.Query<AppReadyGo.Domain.Model.Application>()
                                .Where(a => a.User.Id == this.securityContext.CurrentUser.Id)
                                .Select(p => new ApplicationResult
                                {
                                    Id = p.Id,
                                    Name = p.Name
                                })
                                .ToArray();
            return result;
        }
    }
}