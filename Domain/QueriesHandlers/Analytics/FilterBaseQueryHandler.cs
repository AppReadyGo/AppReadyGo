﻿using System.Drawing;
using System.Linq;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.QueryResults.Analytics;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Domain.Model;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.Queries.Analytics
{
    public abstract class FilterBaseQueryHandler
    {
        protected TResult GetResult<TResult>(ISession session, int userId, IFilterQuery query = null)
            where TResult : FilterDataResult, new()
        {
            var filterData = new TResult();

            filterData.Applications = session.Query<Model.Application>()
                .Where(p => p.User.Id == userId)
                .Select(a => new ExtendedApplicationResult
                {
                    Id = a.Id,
                    Name = a.Description
                })
                .ToArray();

            var applicationsIds = filterData.Applications.Select(p => p.Id).ToArray();

            var sizes = session.Query<PageView>()
                                .Where(p => applicationsIds.Contains(p.Application.Id))
                                .Select(p => new
                                {
                                    key = p.Application.Id,
                                    width = p.ScreenWidth,
                                    height = p.ScreenHeight
                                })
                                .ToArray().Distinct();

            var pathes = session.Query<PageView>()
                                .Where(p => applicationsIds.Contains(p.Application.Id))
                                .Select(p => new
                                {
                                    key = p.Application.Id,
                                    path = p.Path
                                })
                                .ToArray().Distinct();


            foreach (var item in filterData.Applications)
            {
                item.Pathes = pathes.Where(p => p.key == item.Id)
                                    .Select(p => p.path)
                                    .ToArray();
                item.ScreenSizes = sizes.Where(s => s.key == item.Id)
                                        .Select(s => new Size(s.width, s.height))
                                        .ToArray();
            }

            filterData.SelectedApplicationId = query.ApplicationId;
            ExtendedApplicationResult app = filterData.Applications.Single(a => a.Id == filterData.SelectedApplicationId);

            if (query != null)
            {
                filterData.SelectedPath = query.Path;
                filterData.SelectedScreenSize = query.ScreenSize;

                if (string.IsNullOrEmpty(filterData.SelectedPath) && app.Pathes.Any())
                {
                    filterData.SelectedPath = app.Pathes.First();
                }
                if (!filterData.SelectedScreenSize.HasValue && app.ScreenSizes.Any())
                {
                    filterData.SelectedScreenSize = app.ScreenSizes.First();
                }

                if (!string.IsNullOrEmpty(filterData.SelectedPath) && filterData.SelectedScreenSize.HasValue)
                {
                    filterData.ScreenData = new FilterDataResult.Screen();

                    var screen = session.Query<Screen>()
                                        .Where(s => s.Application.Id == filterData.SelectedApplicationId &&
                                                    s.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                    s.Width == filterData.SelectedScreenSize.Value.Width &&
                                                    s.Height == filterData.SelectedScreenSize.Value.Height)
                                        .Select(s => new { Id = s.Id, FileExtention = s.FileExtension })
                                        .FirstOrDefault();
                    if (screen != null)
                    {
                        filterData.ScreenData.Id = screen.Id;
                        filterData.ScreenData.FileExtention = screen.FileExtention;
                    }

                    filterData.ScreenData.ClicksAmount = session.Query<Click>()
                                                        .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId &&
                                                                    s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                    s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                    s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height &&
                                                                    s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                                        .Count();

                    filterData.ScreenData.HasClicks = session.Query<Click>()
                                                        .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId &&
                                                                    s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                    s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                    s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height)
                                                        .Any();

                    filterData.ScreenData.ScrollsAmount = session.Query<Scroll>()
                                                        .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId &&
                                                                    s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                    s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                    s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height &&
                                                                    s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                                        .Count();

                    filterData.ScreenData.VisitsAmount = session.Query<PageView>()
                                                        .Where(p => p.Application.Id == filterData.SelectedApplicationId &&
                                                                    p.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                    p.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                    p.ScreenHeight == filterData.SelectedScreenSize.Value.Height &&
                                                                    p.Date >= query.From && p.Date <= query.To)
                                                        .Count();

                    filterData.ScreenData.HasScrolls = session.Query<Scroll>()
                                                        .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId &&
                                                                    s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                    s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                    s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height)
                                                        .Any();

                    filterData.ScreenData.ControlClicksAmount = session.Query<ControlClick>()
                                                       .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId &&
                                                                   s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                   s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                   s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height &&
                                                                   s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                                       .Count();

                    filterData.ScreenData.HasControlClicks = session.Query<ControlClick>()
                                                        .Where(s => s.PageView.Application.Id == filterData.SelectedApplicationId &&
                                                                    s.PageView.Path.ToLower() == filterData.SelectedPath.ToLower() &&
                                                                    s.PageView.ScreenWidth == filterData.SelectedScreenSize.Value.Width &&
                                                                    s.PageView.ScreenHeight == filterData.SelectedScreenSize.Value.Height)
                                                        .Any();
                }
            }

            filterData.ApiMemberApplications = session.Query<ApiMember>()
                            .SelectMany(u => u.Tasks)
                            //.Where(a => a.Used && a.Application.Id == filterData.SelectedApplicationId)
                            .Select(a => new ApiMemberApplicationResult{
                                UserId = a.User.Id,
                                Email = a.User.Email,
                                FirstName = a.User.FirstName,
                                LastName = a.User.LastName,
                                //Review = a.Review,
                                Type = a.User.Type
                            })
                            .ToArray();

            return filterData;
        }
    }
}
