using AppReadyGo.Core.Entities;
using System.Collections.Generic;

namespace AppReadyGo.Core.Commands.Users
{
    public class CreateMemberCommand : CreateUserCommand
    {
        public CreateMemberCommand(string email, string password)
            : base(email, password)
        {
        }
    }
}
