using AppReadyGo.Core.Entities;
using System.Collections.Generic;

namespace AppReadyGo.Core.Commands.Users
{
    public class CreateStaffCommand : CreateUserCommand
    {
        public CreateStaffCommand(string email, string password)
            : base(email, password)
        {
        }
    }
}
