using AppReadyGo.Core.Commands.Application;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Domain.CommandHandlers.Application
{
    public class ApplicationDownloadedCommandHandler : ICommandHandler<ApplicationDownloadedCommand, int>
    {
        public int Execute(ISession session, ApplicationDownloadedCommand cmd)
        {
            var application = session.Get<Model.Application>(cmd.ApplicationId);
            var user = session.QueryOver<ApiMember>()
                        .Where(u => u.Email.ToLower() == cmd.Email.ToLower())
                        .Select(u => u)
                        .SingleOrDefault();
            user.DownloadApplication(application);
            session.Update(user);
            return user.Id;
        }
    }
}