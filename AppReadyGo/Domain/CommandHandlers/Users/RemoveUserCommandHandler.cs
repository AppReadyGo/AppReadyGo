using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand, int>
    {
        public int Execute(ISession session, RemoveUserCommand cmd)
        {
            var user = session.Get<User>(cmd.Id);
            session.Delete(user);
            return user.Id;
        }
    }
}
