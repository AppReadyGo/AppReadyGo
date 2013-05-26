using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.API
{
    public class ApplicationDownloadedCommand : ICommand<int>
    {
        public int MemberId { get; protected set; }

        public int ApplicationId { get; protected set; }

        public ApplicationDownloadedCommand(int memberId, int ApplicationId)
        {
            this.MemberId = memberId;
            this.ApplicationId = ApplicationId;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            yield break;
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}