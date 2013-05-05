using System;
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

            var firstSession = cmd.Sessions.First();


            var pageView = new PageView(application, DateTime.UtcNow, firstSession.Path, null, null, null, null, operationSystem, null, cmd.ScreenWidth, cmd.ScreenHeight, firstSession.ClientWidth, firstSession.ClientHeight);
            var clicks = firstSession.Clicks.Select(c => new Click(
                pageView,
                c.Date,
                c.ClientX,
                c.ClientY,
                c.Orientation   
            ));

            var viewParts = firstSession.ScreenViewParts.Select(vp => new ViewPart(
                pageView,
                vp.StartDate,
                vp.FinishDate,
                vp.ScrollLeft,
                vp.ScrollTop,
                vp.Orientation
            ));

            var scrolls = firstSession.Scrolls.Select(s => new Scroll(
                pageView,
                clicks.FirstOrDefault(c => c.Date == s.FirstTouch.Date),
                clicks.FirstOrDefault(c => c.Date == s.LastTouch.Date)
            ));

            application.AddPageView(pageView);

            session.SaveOrUpdate(pageView);

            return pageView.Id;
        }
    }
}
