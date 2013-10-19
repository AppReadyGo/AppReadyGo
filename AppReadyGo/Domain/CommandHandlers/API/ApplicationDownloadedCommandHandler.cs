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
    public class ApplicationDownloadedCommandHandler : ICommandHandler<ApplicationDownloadedCommand, int>
    {
        public int Execute(ISession session, ApplicationDownloadedCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var user = session.Get<ApiMember>(cmd.MemberId);
            user.DownloadApplication(application);
            session.Update(user);
            return user.Id;
        }
    }
}