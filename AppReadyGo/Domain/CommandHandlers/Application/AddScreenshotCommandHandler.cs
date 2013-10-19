using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class AddScreenshotCommandHandler : ICommandHandler<AddScreenshotCommand, int>
    {
        public int Execute(ISession session, AddScreenshotCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var screenshot = new Model.Screenshot(application, cmd.FileExtention);
            session.Save(screenshot);
            return screenshot.Id;
        }
    }
}
