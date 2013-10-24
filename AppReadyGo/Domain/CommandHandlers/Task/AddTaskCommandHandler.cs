using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;
using AppReadyGo.Core.Commands.Task;

namespace AppReadyGo.Domain.CommandHandlers.Tasks
{
    public class AddTaskCommandHandler : ICommandHandler<AddTaskCommand, int>
    {
        public int Execute(ISession session, AddTaskCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var country = cmd.CountryId.HasValue ? session.Get<Model.Country>(cmd.CountryId) : null;
            var desc = session.Get<Model.TaskDescription>(cmd.DescId);
            var task = new Task(desc, application, cmd.AgeRange, cmd.Gender, country, cmd.Zip);
            if (cmd.Publish)
            {
                task.Publish();
            }
            session.Save(task);
            return task.Id;
        }
    }
}
