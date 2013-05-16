using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.Users
{
    public abstract class UpdateUserCommand : ICommand<int>
    {
        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public int UserId { get; set; }

        protected UpdateUserCommand(int userId, string email, string password)
        {
            this.UserId = userId;
            this.Email = email;
            this.Password = password;
        }

        public virtual IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                yield return new ValidationResult(ErrorCode.WrongEmail, "The command must have an Email parameter.");
            }

            if (!validation.IsCorrectEmail(this.Email))
            {
                yield return new ValidationResult(ErrorCode.WrongEmail, "The email is wrong.");
            }

            if (validation.IsEmailExists(this.Email, this.UserId))
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
