using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Tasks;

namespace AppReadyGo.Domain.CommandHandlers.Tasks
{
    public class UnPublishTaskCommandHandler : ICommandHandler<UnPublishTaskCommand, object>
    {
        public object Execute(ISession session, UnPublishTaskCommand cmd)
        {
            var task = session.Get<Model.Task>(cmd.Id);
            task.Publish(false);
            session.Save(task);
            return null;
        }
    }
}
