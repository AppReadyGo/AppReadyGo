using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Application;
using AppReadyGo.Core;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand, int>
    {
        private ISecurityContext securityContext;

        public CreateApplicationCommandHandler(ISecurityContext securityContext)
        {
            this.securityContext = securityContext;
        }

        public int Execute(ISession session, CreateApplicationCommand cmd)
        {
            var user = session.Get<Member>(securityContext.CurrentUser.Id);
            var type = session.Get<ApplicationType>(cmd.Type);
            var app = new Model.Application(user, cmd.Name, cmd.Description, type, cmd.IconExt);
            session.Save(app);
            session.Save(user);
            return app.Id;
        }
    }
}
