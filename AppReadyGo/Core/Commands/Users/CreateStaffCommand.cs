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

        public override IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            foreach (var val in base.Validate(validation))
            {
                yield return val;
            }

            if (validation.IsEmailExists(this.Email, null, UserType.Member, UserType.Staff))
            {
                yield return new ValidationResult(ErrorCode.EmailExists, "The email exists in the system.");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                yield return new ValidationResult(ErrorCode.WrongPassword, "The command must have an Password parameter.");
            }

            if (!validation.IsCorrectPassword(this.Password))
            {
                yield return new ValidationResult(ErrorCode.WrongPassword, "The password is wrong.");
            }
        }
    }
}
