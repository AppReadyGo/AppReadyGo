using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class UpdateApplicationIconCommandHandler : ICommandHandler<UpdateApplicationIconCommand, string>
    {
        public string Execute(ISession session, UpdateApplicationIconCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.ApplicationId);
            string oldExt = app.IconExt;
            app.UpdateIcon(cmd.IconExt);
            return oldExt;
        }
    }
}
