using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class UpdateScreenCommandHandler : ICommandHandler<UpdateScreenCommand, int>
    {
        public int Execute(ISession session, UpdateScreenCommand cmd)
        {
            var screen = session.Get<Model.Screen>(cmd.Id);
            screen.Update(cmd.Path, cmd.Width, cmd.Height, cmd.FileExtention);
            session.Update(screen);
            return screen.Id;
        }
    }
}
