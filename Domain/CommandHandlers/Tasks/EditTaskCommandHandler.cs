using AppReadyGo.Core.Commands;
using AppReadyGo.Domain.Model;
using NHibernate;
using AppReadyGo.Core.Commands.Applications;
using AppReadyGo.Core.Commands.Tasks;
using AppReadyGo.Core;

namespace AppReadyGo.Domain.CommandHandlers.Tasks
{
    public class EditTaskCommandHandler : ICommandHandler<EditTaskCommand, object>
    {
        public object Execute(ISession session, EditTaskCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var country = cmd.CountryId.HasValue ? session.Get<Model.Country>(cmd.CountryId) : null;
            var desc = session.Get<Model.TaskDescription>(cmd.DescId);
            var task = session.Get<Model.Task>(cmd.Id);
            task.Update(desc, application, cmd.AgeRange, cmd.Gender, country, cmd.Zip, cmd.Audence);
            if (cmd.Publish.HasValue)
            {
                task.Publish(cmd.Publish.Value);
            }
            session.Save(task);
            return null;
        }
    }
}
