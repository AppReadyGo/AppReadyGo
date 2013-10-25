using System.Collections.Generic;
using System.Linq;
using AppReadyGo.Core.Queries;
using AppReadyGo.Domain.Model.Content;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Domain.Model;
using System;

namespace AppReadyGo.Domain.Queries
{
    public class GetApplicationsForUserQueryHandler : IQueryHandler<GetApplicationsForUserQuery, PagingResult<APIApplicationResult>>
    {
        public PagingResult<APIApplicationResult> Run(ISession session, GetApplicationsForUserQuery query)
        {
            Random rd = new Random();

            var res = new PagingResult<APIApplicationResult>
            {
                CurPage = query.CurPage,
                PageSize = query.PageSize
            };

            var appsQuery = session.Query<AppReadyGo.Domain.Model.Application>().Where(a => a.Tasks.Any(t => t.PublishDate != null));

            res.ItemsCount = appsQuery.Count();

            int maxItems = res.ItemsCount <= query.PageSize ? res.ItemsCount : query.PageSize;

            var ids = appsQuery.Select(a => a.Id).ToList();
            var selIds = Enumerable.Range(1, maxItems).Select(i =>
            {
                int rnd = rd.Next(ids.Count);
                int r = ids[rnd];
                ids.Remove(r);
                return r;
            })
            .ToArray();

            res.Collection = appsQuery
                                .Where(a => selIds.Contains(a.Id))
                                .Select(a => new APIApplicationResult
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            HasIcon = a.IconExt != null,
                            Url = a.MarketUrl
                        })
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

            foreach (var app in res.Collection)
            {
                app.Screenshots = screenshots.Where(s => s.AppId == app.Id).ToDictionary(k => k.Id, v => v.Ext);
            }

            return res;
        }

        // TODO: The query will be used in future
        //public PagingResult<APIApplicationResult> Run(ISession session, GetApplicationsForUserQuery query)
        //{
        //    var res = new PagingResult<APIApplicationResult>
        //    {
        //        CurPage = query.CurPage,
        //        PageSize = query.PageSize
        //    };

        //    var userDetails = session.Query<ApiMember>()
        //                        .Where(m => m.Id == query.UserId)
        //                        .Select(m => new {
        //                            m.Zip,
        //                            m.Gender,
        //                            CountryId = m.Country == null ? null : (int?)m.Country.GeoId,
        //                            m.AgeRange
        //                        })
        //                        .Single();

        //    var publishQuery = session.Query<PublishDetails>();
        //    if (userDetails.Gender.HasValue && userDetails.Gender.Value != Core.Entities.Gender.None)
        //    {
        //        publishQuery = publishQuery.Where(p => p.Gender == userDetails.Gender.Value || p.Gender == Core.Entities.Gender.None);
        //    }

        //    if (!string.IsNullOrEmpty(userDetails.Zip))
        //    {
        //        publishQuery = publishQuery.Where(p => p.Zip == userDetails.Zip || p.Zip == null);
        //    }

        //    if (userDetails.CountryId.HasValue)
        //    {
        //        publishQuery = publishQuery.Where(p => p.Country.GeoId == userDetails.CountryId || p.Country == null);
        //    }

        //    if (userDetails.AgeRange.HasValue && userDetails.AgeRange.Value != Core.Entities.AgeRange.None)
        //    {
        //        publishQuery = publishQuery.Where(p => p.AgeRange == userDetails.AgeRange || p.AgeRange == Core.Entities.AgeRange.None);
        //    }
        //    var appsIds = publishQuery.Select(p => p.Application.Id).Distinct().ToArray();

        //    var appsQuery = session.Query<AppReadyGo.Domain.Model.Application>()
        //                            .Where(a => appsIds.Contains(a.Id));

        //    res.ItemsCount = appsQuery.Count();

        //    var skip = query.PageSize * (query.CurPage - 1);
        //    res.Collection = appsQuery.Select(a => new APIApplicationResult
        //                {
        //                        Id = a.Id,
        //                        Name = a.Name,
        //                        Description = a.Description,
        //                        HasIcon = a.IconExt != null,
        //                        Url = a.MarketUrl
        //                })
        //                .Skip(skip)
        //                .Take(query.PageSize)
        //                .ToArray();

        //    var appIds = res.Collection.Select(a => a.Id).ToArray();

        //    var screenshots = session.Query<AppReadyGo.Domain.Model.Screenshot>()
        //                                .Where(s => appIds.Contains(s.Application.Id))
        //                                .Select(s => new 
        //                                { 
        //                                    AppId = s.Application.Id, 
        //                                    Id = s.Id, 
        //                                    Ext = s.FileExtension 
        //                                })
        //                                .ToArray();
        //    foreach( var app in res.Collection)
        //    {
        //        app.Screenshots = screenshots.Where(s => s.AppId == app.Id).ToDictionary(k => k.Id, v => v.Ext);
        //    }
                                        
        //    return res;
        //}
    }
}
