using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Application;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class PublishCommandHandler : ICommandHandler<PublishCommand, int>
    {
        public int Execute(ISession session, PublishCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var country = session.Get<Model.Application>(cmd.CountryId);
            var pkg = new PublishDetails(cmd.AgeRange, cmd.Gender, country, cmd.Zip);
            application.SetPakage(pkg);
            session.Save(pkg);
            return pkg.Id;
        }
    }
}
