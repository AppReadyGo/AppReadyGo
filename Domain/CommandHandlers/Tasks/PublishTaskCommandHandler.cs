using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Tasks;

namespace AppReadyGo.Domain.CommandHandlers.Tasks
{
    public class PublishTaskCommandHandler : ICommandHandler<PublishTaskCommand, object>
    {
        public object Execute(ISession session, PublishTaskCommand cmd)
        {
            var task = session.Get<Model.Task>(cmd.Id);
            task.Publish();
            session.Save(task);
            return null;
        }
    }
}
