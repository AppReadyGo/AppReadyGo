using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class UnPublishCommandHandler : ICommandHandler<UnPublishCommand, int>
    {
        public int Execute(ISession session, UnPublishCommand cmd)
        {
            var publishDetails = session.Get<Model.Task>(cmd.Id); ;
            session.Delete(publishDetails);
            return publishDetails.Application.Id;
        }
    }
}
