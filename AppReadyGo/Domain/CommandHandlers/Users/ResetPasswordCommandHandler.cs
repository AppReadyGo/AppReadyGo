using System.Linq;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;

namespace AppReadyGo.Domain.CommandHandlers.Users
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, int?>
    {
        public int? Execute(ISession session, ResetPasswordCommand cmd)
        {
            var user = session.Query<User>()
                            .Where(u => u.Email.ToLower() == cmd.Email.ToLower())
                            .Select(u => u)
                            .SingleOrDefault();
            if (user != null)
            {
                user.ChangePassword(cmd.Password);
                return user.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
