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
            var country = cmd.CountryId.HasValue ? session.Get<Model.Country>(cmd.CountryId) : null;
            var publishDetails = new PublishDetails(application, cmd.AgeRange, cmd.Gender, country, cmd.Zip);
            session.Save(publishDetails);
            return publishDetails.Id;
        }
    }
}
