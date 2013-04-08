using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Commands.Users;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class UpdateLastAccessCommandHandler : ICommandHandler<UpdateLastAccessCommand, bool>
    {
        public bool Execute(ISession session, UpdateLastAccessCommand cmd)
        {
            var user = session.Query<User>()
                            .Where(u => u.Id == cmd.UserId)
                            .Select(u => u)
                            .SingleOrDefault();
            if (user != null)
            {
                user.UpdateLastAccess();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
