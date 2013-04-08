using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Application;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class AddScreenCommandHandler : ICommandHandler<AddScreenCommand, long>
    {
        public long Execute(ISession session, AddScreenCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var screen = new Model.Screen(application, cmd.Path, cmd.Width, cmd.Height, cmd.FileExtention);
            application.AddScreen(screen);
            session.Save(screen);
            return screen.Id;
        }
    }
}
