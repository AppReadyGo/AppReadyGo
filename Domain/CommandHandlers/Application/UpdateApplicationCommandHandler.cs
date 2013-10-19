using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class UpdateApplicationCommandHandler : ICommandHandler<UpdateApplicationCommand, int>
    {
        public int Execute(ISession session, UpdateApplicationCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.Id);
            var type = session.Get<Model.ApplicationType>(cmd.TypeId);
            app.Update(cmd.Name, cmd.Description, type);
            return app.Id;
        }
    }
}
