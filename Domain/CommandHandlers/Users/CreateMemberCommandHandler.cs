using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Domain.Model.Users;
using AppReadyGo.Domain.Model;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, int>
    {
        public int Execute(ISession session, CreateMemberCommand cmd)
        {
            var user = new Member(cmd.Email, cmd.Password);
            session.Save(user);
            return user.Id;
        }
    }
}
