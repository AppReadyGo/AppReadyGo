using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class UpdatePackageCommandHandler : ICommandHandler<UpdatePackageCommand, string>
    {
        public string Execute(ISession session, UpdatePackageCommand cmd)
        {
            var app = session.Get<Model.Application>(cmd.ApplicationId);
            string oldFileName = app.PackageFileName;
            app.UpdatePackage(cmd.FileName);
            return oldFileName;
        }
    }
}
