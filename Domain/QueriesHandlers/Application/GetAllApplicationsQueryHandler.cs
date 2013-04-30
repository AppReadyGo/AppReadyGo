using System;
using System.Linq;
using System.Reflection;
using AppReadyGo.Core;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.QueryResults.Application;
using AppReadyGo.Domain.Model;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Drawing;
using AppReadyGo.Core.QueryResults;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.Queries.Application
{
    public class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, ApplicationsDataResult>
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        private ISecurityContext securityContext;

        public GetAllApplicationsQueryHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public ApplicationsDataResult Run(ISession session, GetAllApplicationsQuery query)
        {
            log.WriteInformation("-> Get all applications for SearchStr:{0}, PageSize:{1}, CurPage:{2}, User:{3}", query.SearchStr, query.PageSize, query.CurPage, securityContext.CurrentUser.Email);
            var res = new ApplicationsDataResult();

            var applicationsQuery = session.Query<Model.Application>()
                        .Where(a => a.User.Id == securityContext.CurrentUser.Id);

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                applicationsQuery = applicationsQuery.Where(a => a.Description.ToLower().Contains(query.SearchStr.ToLower()));
            }

            res.Count = applicationsQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;

            res.Applications = applicationsQuery.Select(a => new ApplicationDataItemResult
                                            {
                                                Id = a.Id,
                                                Name = a.Name,
                                                Type = a.Type.Name,
                                                Description = a.Description,
                                                IconExt = a.IconExt
                                            })
                                            .ToArray();

            res.Applications = res.Applications.Skip(res.PageSize * (res.CurPage - 1))
                                    .Take(res.PageSize)
                                    .ToArray();
            
            //var visits = session.Query<PageView>()
            //                    .Where(p => p.Application.User.Id == securityContext.CurrentUser.Id)
            //                    .GroupBy(p => p.Application.Id)
            //                    .Select(g => new
            //                    {
            //                        Key = g.Key,
            //                        VisitsCount = g.Count(),
            //                        LastRecivedDataDate = g.Max(x => x.Date)
            //                    })
            //                    .ToArray();

            // Get top applications and top screens
            var visitsByScreens = session.Query<PageView>()
                    .Where(p => p.Application.User.Id == securityContext.CurrentUser.Id)
                    .GroupBy(p => new
                    {
                        ApplicationId = p.Application.Id,
                        Name = p.Application.Name,
                        Path = p.Path,
                        ScreenHeight = p.ScreenHeight,
                        ScreenWidth = p.ScreenWidth
                    })
                    .Select(g => new
                    {
                        Key = g.Key,
                        VisitsCount = g.Count(),
                        LastRecivedDataDate = g.Max(x => x.Date)
                    })
                    .ToArray();

            var visits = visitsByScreens.GroupBy(g => new
                                {
                                    ApplicationId = g.Key.ApplicationId,
                                    Name = g.Key.Name,
                                })
                                .Select(g => new
                                {
                                    Key = g.Key,
                                    VisitsCount = g.Count(),
                                    LastRecivedDataDate = g.Max(x => x.LastRecivedDataDate)
                                })
                                .ToArray();

            res.TopScreens = visitsByScreens.OrderByDescending(g => g.VisitsCount)
                                            .Take(5)
                                            .Select(g => new ApplicationScreenResult
                                            {
                                                ApplicationId = g.Key.ApplicationId,
                                                Path = g.Key.Path,
                                                ScreenSize = new Size(g.Key.ScreenWidth, g.Key.ScreenHeight)
                                            }).ToArray();

            res.TopApplications = visits.OrderByDescending(g => g.VisitsCount)
                                            .Take(5)
                                            .Select(g => new ApplicationResult
                                            {
                                                Id = g.Key.ApplicationId,
                                                Name = g.Key.Name
                                            }).ToArray();

            var appIds = res.Applications.Select(x => x.Id).ToArray();
            var appPublished = session.Query<Model.PublishDetails>()
                        .Where(p => appIds.Contains(p.Application.Id))
                        .Select(p => p.Application.Id)
                        .ToArray();

            var appDownloaded = session.Query<ApiMember>()
                        .SelectMany(u => u.DownloadedApplications)
                        .Where(a => appIds.Contains(a.Id))
                        .Select(a => a.Id)
                        .GroupBy(id => id)
                        .Select(x => new { ApplicationId = x.Key, Count = x.Count() })
                        .ToArray();

            // Aplicatyion is not active if was not recived data for 3 days
            DateTime dt = DateTime.Now.AddDays(-3);

            foreach (var application in res.Applications)
            {
                var count = visits.SingleOrDefault(a => a.Key.ApplicationId == application.Id);

                application.Visits = count != null ? count.VisitsCount : 0;

                application.IsActive = count != null && count.LastRecivedDataDate < dt ? true : false;

                application.Published = appPublished.Contains(application.Id);

                application.Downloaded = appDownloaded.Where(x => x.ApplicationId == application.Id).Select(x => x.Count).SingleOrDefault();
            }
            log.WriteInformation("Get all applications for portfolio ->");

            return res;
        }
    }
}
