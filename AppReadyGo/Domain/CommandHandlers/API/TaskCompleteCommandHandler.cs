using AppReadyGo.Core.Commands.Applications;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.Commands.API;
using AppReadyGo.Domain.Model;

namespace AppReadyGo.Domain.CommandHandlers.API
{
    public class TaskCompleteCommandHandler : ICommandHandler<TaskCompleteCommand, int>
    {
        public int Execute(ISession session, TaskCompleteCommand cmd)
        {
            var task = session.Query<ApiMemberTask>()
                            .Where(t => t.Task.Id == cmd.TaskId && t.User.Id == cmd.MemberId)
                            .Single();
            task.Complete();
            session.Update(task);
            return task.Id;
        }
    }
}