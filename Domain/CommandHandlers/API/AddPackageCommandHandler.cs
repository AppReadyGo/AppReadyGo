﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Commands.API;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Domain.Model;

namespace AppReadyGo.Domain.CommandHandlers.API
{
    public class AddPackageCommandHandler : ICommandHandler<AddPackageCommand, long>
    {
        public long Execute(ISession session, AddPackageCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var operationSystem = session.Query<OperationSystem>().
                                    Where(os => os.Name.ToLower() == cmd.SystemInfo.RealVersionName). //check which name to use!
                                    FirstOrDefault();

            foreach (var item in cmd.Sessions)
            {
                var pageView = new PageView(application, DateTime.UtcNow, item.Path, null, null, null, null, operationSystem, null, cmd.ScreenWidth, cmd.ScreenHeight, item.ClientWidth, item.ClientHeight, cmd.TaskId, cmd.UserId);
                var clicks = item.Clicks.Select(c => new Click(
                    pageView,
                    c.Date,
                    c.ClientX,
                    c.ClientY,
                    c.Orientation
                ));

                var viewParts = item.ScreenViewParts.Select(vp => new ViewPart(
                    pageView,
                    vp.StartDate,
                    vp.FinishDate,
                    vp.ScrollLeft,
                    vp.ScrollTop,
                    vp.Orientation
                ));

                var scrolls = item.Scrolls.Select(s => new Scroll(
                    pageView,
                    clicks.FirstOrDefault(c => c.Date == s.FirstTouch.Date),
                    clicks.FirstOrDefault(c => c.Date == s.LastTouch.Date)
                ));

                var controlClicks = item.ControlClicks.Select(cc => new ControlClick(
                    pageView,
                    cc.Date,
                    cc.Tag
                ));

                application.AddPageView(pageView);

                foreach (var click in clicks)
                {
                    session.SaveOrUpdate(click);
                }

                foreach (var part in viewParts)
                {
                    session.SaveOrUpdate(part);
                }

                foreach (var scroll in scrolls)
                {
                    session.SaveOrUpdate(scroll);
                }

                foreach (var controlClick in controlClicks)
                {
                    session.SaveOrUpdate(controlClick);
                }

                session.SaveOrUpdate(pageView);

                session.Flush();
            }

            return cmd.Sessions.Count();
        }
    }
}
