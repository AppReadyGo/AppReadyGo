using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand, bool>
    {
        public bool Execute(ISession session, DeactivateUserCommand cmd)
        {
            try
            {
                var user = session.Get<User>(cmd.Id);
                if (user != null)
                {
                    user.Deactivate();
                    return true;
                }   
            }
            catch (Exception)
            {

                return false;
            }
            return false;
        }
    }
}
