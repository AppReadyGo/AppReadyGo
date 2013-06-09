using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Domain.Queries
{
    public class GetApplicationsForUserQueryHandler : IQueryHandler<GetApplicationsForUserQuery, PagingResult<APIApplicationResult>>
    {
        public PagingResult<APIApplicationResult> Run(ISession session, GetApplicationsForUserQuery query)
        {
            var res = new PagingResult<APIApplicationResult>
            {
                CurPage = query.CurPage,
                PageSize = query.PageSize
            };

            // TODO: Change application filtering by user details
            var appsQuery = session.Query<AppReadyGo.Domain.Model.Application>();
            res.ItemsCount = appsQuery.Count();

            var skip = query.PageSize * (query.CurPage - 1);
            res.Collection = appsQuery.Select(a => new APIApplicationResult
                        {
                                Id = a.Id,
                                Name = a.Name,
                                Description = a.Description,
                                HasIcon = a.IconExt != null,
                                Url = a.MarketUrl
                        })
                        .Skip(skip)
                        .Take(query.PageSize)
                        .ToArray();

            var appIds = res.Collection.Select(a => a.Id).ToArray();

            var screenshots = session.Query<AppReadyGo.Domain.Model.Screenshot>()
                                        .Where(s => appIds.Contains(s.Application.Id))
                                        .Select(s => new 
                                        { 
                                            AppId = s.Application.Id, 
                                            Id = s.Id, 
                                            Ext = s.FileExtension 
                                        })
                                        .ToArray();
            foreach( var app in res.Collection)
            {
                app.Screenshots = screenshots.Where(s => s.AppId == app.Id).ToDictionary(k => k.Id, v => v.Ext);
            }
                                        
            return res;
        }
    }
}
