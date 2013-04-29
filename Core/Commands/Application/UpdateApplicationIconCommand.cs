using System.Collections.Generic;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Application
{
    public class UpdateApplicationIconCommand : ICommand<string>
    {
        public int ApplicationId { get; protected set; }
        public string IconExt { get; protected set; }

        public UpdateApplicationIconCommand(int applicationId, string iconExt)
        {
            this.ApplicationId = applicationId;
            this.IconExt = iconExt;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.IconExt))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have IconExt parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}
