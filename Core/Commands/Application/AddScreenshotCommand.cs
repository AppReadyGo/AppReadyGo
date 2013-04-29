using System.Collections.Generic;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Application
{
    public class AddScreenshotCommand : ICommand<int>
    {
        public int ApplicationId { get; protected set; }
        public string FileExtention { get; protected set; }

        public AddScreenshotCommand(int applicationId, string fileExtention)
        {
            this.ApplicationId = applicationId;
            this.FileExtention = fileExtention;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.FileExtention))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have FileExtention parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}
