using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Application;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class UpdateApplicationCommandHandler : ICommandHandler<UpdateApplicationCommand, int>
    {
        public int Execute(ISession session, UpdateApplicationCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.Id);
            app.Update(cmd.Description);
            return app.Id;
        }
    }
}
