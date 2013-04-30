using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.Application
{
    public class ApplicationDownloadedCommand : ICommand<int>
    {
        public string Email { get; protected set; }

        public int ApplicationId { get; protected set; }

        public ApplicationDownloadedCommand(string email, int ApplicationId)
        {
            this.Email = email;
            this.ApplicationId = ApplicationId;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have Email parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}