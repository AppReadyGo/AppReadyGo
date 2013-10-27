using System.Linq;
using AppReadyGo.Core.Queries.Admin;
using AppReadyGo.Core.QueryResults.Admin;
using AppReadyGo.Core.QueryResults.Users;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries.Admin
{
    public class GetAllApplicationsQueryHandler : IQueryHandler<GetAllApplicationsQuery, AllApplicationsResult>
    {
        public AllApplicationsResult Run(ISession session, GetAllApplicationsQuery query)
        {
            var res = new AllApplicationsResult();

            var applicationsQuery = session.Query<AppReadyGo.Domain.Model.Application>();

            if (!string.IsNullOrEmpty(query.SearchStr))
            {
                var str = query.SearchStr.ToLower();
                applicationsQuery = applicationsQuery.Where(a => a.Name.ToLower().Contains(str) || a.Description.ToLower().Contains(str));
            }

            res.Count = applicationsQuery.Count();
            res.TotalPages = (res.Count + query.PageSize - 1) / query.PageSize;
            res.CurPage = query.CurPage > res.TotalPages ? res.TotalPages : query.CurPage;
            res.PageSize = query.PageSize;


            var users = applicationsQuery.Select(a => new ApplicationFullDetailsResult
            {
                Id = a.Id,
                UserId = a.User.Id,
                Type = a.Type.Name,
                Name = a.Name,
                IconExt = a.IconExt,
                PackageFileName = a.PackageFileName,
                Published = a.Tasks.Count() > 0,
                Description = a.Description,
                CreateDate = a.CreateDate,
                UserEmail = a.User.Email
            });

            if (query.OrderBy == GetAllApplicationsQuery.OrderByColumn.Name)
            {
                users = query.ASC ? users.OrderBy(a => a.Name) : users.OrderByDescending(a => a.Name);
            }
            else if (query.OrderBy == GetAllApplicationsQuery.OrderByColumn.Type)
            {
                users = query.ASC ? users.OrderBy(u => u.Type) : users.OrderByDescending(u => u.Type);
            }
            else if (query.OrderBy == GetAllApplicationsQuery.OrderByColumn.CreateDate)
            {
                users = query.ASC ? users.OrderBy(u => u.CreateDate) : users.OrderByDescending(u => u.CreateDate);
            }

            res.Applications = users.Skip(res.PageSize * (res.CurPage - 1))
                                    .Take(res.PageSize)
                                    .ToArray();

            var appIds = res.Applications.Select(a => a.Id).ToArray();
            var downloaded = session.Query<AppReadyGo.Domain.Model.Users.ApiMember>()
                                        .SelectMany(m => m.Tasks)
                                        .Where(a => appIds.Contains(a.Id))
                                        .Select(a => a.Id)
                                        .GroupBy(x => x)
                                        .Select(x => new { Id = x.Key, Count = x.Count() })
                                        .ToDictionary(k => k.Id, v => v.Count);

            var screens = session.Query<AppReadyGo.Domain.Model.Screen>()
                                        .Where(s => appIds.Contains(s.Application.Id))
                                        .Select(a => a.Application.Id)
                                        .GroupBy(x => x)
                                        .Select(x => new { Id = x.Key, Count = x.Count() })
                                        .ToDictionary(k => k.Id, v => v.Count);

            var screenshots = session.Query<AppReadyGo.Domain.Model.Screenshot>()
                                        .Where(s => appIds.Contains(s.Application.Id))
                                        .Select(a => a.Application.Id)
                                        .GroupBy(x => x)
                                        .Select(x => new { Id = x.Key, Count = x.Count() })
                                        .ToDictionary(k => k.Id, v => v.Count);

            var pageViews = session.Query<AppReadyGo.Domain.Model.PageView>()
                                        .Where(s => appIds.Contains(s.Application.Id))
                                        .Select(a => a.Application.Id)
                                        .GroupBy(x => x)
                                        .Select(x => new { Id = x.Key, Count = x.Count() })
                                        .ToDictionary(k => k.Id, v => v.Count);

            foreach (var app in res.Applications)
            {
                app.Downloaded = downloaded.ContainsKey(app.Id) ? downloaded[app.Id] : 0;
                app.Screens = screens.ContainsKey(app.Id) ? screens[app.Id] : 0;
                app.Screenshots = screenshots.ContainsKey(app.Id) ? screenshots[app.Id] : 0;
                app.Visits = pageViews.ContainsKey(app.Id) ? pageViews[app.Id] : 0;
                app.HasScreens = screens.ContainsKey(app.Id);
                app.IsActive = pageViews.ContainsKey(app.Id);
            }

            return res;
        }
    }
}
