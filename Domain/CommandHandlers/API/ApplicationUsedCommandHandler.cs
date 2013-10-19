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

namespace AppReadyGo.Domain.CommandHandlers.API
{
    public class ApplicationUsedCommandHandler : ICommandHandler<ApplicationUsedCommand, int>
    {
        public int Execute(ISession session, ApplicationUsedCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var user = session.Get<ApiMember>(cmd.MemberId);
            user.UserApplicationWasUsed(application);
            session.Update(user);
            return user.Id;
        }
    }
}