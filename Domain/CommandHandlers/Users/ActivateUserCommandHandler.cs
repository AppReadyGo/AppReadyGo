﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Commands.Users;
using NHibernate;
using NHibernate.Linq;
using AppReadyGo.Domain.Model.Users;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, int?>
    {
        public int? Execute(ISession session, ActivateUserCommand cmd)
        {
            var user = session.Query<User>()
                            .Where(u => u.Email.ToLower() == cmd.Email.ToLower() && cmd.UserTypes.Contains(u.Type))
                            .Select(u => u)
                            .SingleOrDefault();
            if (user != null)
            {
                user.Activate();
                return user.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
