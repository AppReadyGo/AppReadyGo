using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Application;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class SetPackageCommandHandler : ICommandHandler<SetPackageCommand, int>
    {
        public int Execute(ISession session, SetPackageCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var pkg = new Package(cmd.FileName);
            application.SetPakage(pkg);
            session.Save(pkg);
            return pkg.Id;
        }
    }
}
