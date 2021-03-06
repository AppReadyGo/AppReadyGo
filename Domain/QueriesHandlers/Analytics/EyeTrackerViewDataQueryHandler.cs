﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Queries.Analytics;
using NHibernate.Linq;
using NHibernate;
using AppReadyGo.Domain.Model;
using AppReadyGo.Core;
using AppReadyGo.Core.QueryResults.Analytics.QueryResults;
using AppReadyGo.Core.Commands;
using AppReadyGo.Core.QueryResults.Analytics;

namespace AppReadyGo.Domain.Queries.Analytics
{
    public class EyeTrackerViewDataQueryHandler : FilterBaseQueryHandler, IQueryHandler<EyeTrackerViewDataQuery, EyeTrackerViewDataResult>
    {
        private IRepository repository;
        private ISecurityContext securityContext;

        public EyeTrackerViewDataQueryHandler(IRepository repository, ISecurityContext securityContext)
        {
            this.repository = repository;
            this.securityContext = securityContext;
        }

        public EyeTrackerViewDataResult Run(ISession session, EyeTrackerViewDataQuery query)
        {
            var data = GetResult<EyeTrackerViewDataResult>(session, this.securityContext.CurrentUser.Id, query);
            data.Screens = session.Query<Screen>()
                                        .Where(s => s.Application.Id == data.SelectedApplicationId)
                                        .Select(s => new ScreenResult
                                        {
                                            Id = s.Id,
                                            Path = s.Path,
                                            ApplicationId = s.Application.Id,
                                            Height = s.Height,
                                            Width = s.Width,
                                            FileExtension = s.FileExtension
                                        })
                                        .ToArray();

            if (!string.IsNullOrWhiteSpace(data.SelectedPath) && data.SelectedScreenSize.HasValue)
            {
                data.UsageData = session.Query<Scroll>()
                                        .Where(s => s.PageView.Application.Id == data.SelectedApplicationId &&
                                                    s.PageView.Path.ToLower() == data.SelectedPath.ToLower() &&
                                                    s.PageView.ScreenWidth == data.SelectedScreenSize.Value.Width &&
                                                    s.PageView.ScreenHeight == data.SelectedScreenSize.Value.Height &&
                                                    s.PageView.Date >= query.From && s.PageView.Date <= query.To)
                                        .GroupBy(c => c.PageView.Date)
                                        .Select(g => new KeyValuePair<DateTime, int>(g.Key, g.Count()))
                                        .ToList().ToDictionary(v => v.Key, v => v.Value);
            }
            else
            {
                data.UsageData = new Dictionary<DateTime, int>();
            }
            return data;
        }
    }
}
