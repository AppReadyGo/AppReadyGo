using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Application;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class RemoveApplicationCommandHandler : ICommandHandler<RemoveApplicationCommand, int>
    {
        public int Execute(ISession session, RemoveApplicationCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.Id);
            session.Delete(app);
            return app.Id;
        }
    }
}
