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
    public class StartTaskCommandHandler : ICommandHandler<StartTaskCommand, int>
    {
        public int Execute(ISession session, StartTaskCommand cmd)
        {
            var task = session.Get<Model.Task>(cmd.TaskId);
            var user = session.Get<ApiMember>(cmd.MemberId);

            var userTask = new ApiMemberTask(task, user);
            user.StartTask(userTask);
            session.Update(user);
            return user.Id;
        }
    }
}