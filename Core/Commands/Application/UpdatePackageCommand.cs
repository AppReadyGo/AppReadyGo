using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.Application
{
    public class UpdatePackageCommand : ICommand<string>
    {
        public int ApplicationId { get; protected set; }
        public string FileName { get; protected set; }

        public UpdatePackageCommand(int applicationId, string fileName)
        {
            this.ApplicationId = applicationId;
            this.FileName = fileName;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.FileName))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have FileName parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}
