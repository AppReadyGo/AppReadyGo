using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class RemoveScreenCommandHandler : ICommandHandler<RemoveScreenCommand, int>
    {
        public int Execute(ISession session, RemoveScreenCommand cmd)
        {
            var screen = session.Get<Model.Screen>(cmd.Id);
            screen.Application.RemoveScreen(screen);
            session.Delete(screen);
            return screen.Id;
        }
    }
}
