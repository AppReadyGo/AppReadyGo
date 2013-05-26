using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.API
{
    public class ApplicationUpdateReviewCommand : ICommand<int>
    {
        public int MemberId { get; protected set; }

        public int ApplicationId { get; protected set; }

        public string Review { get; protected set; }

        public ApplicationUpdateReviewCommand(int userId, int ApplicationId, string review)
        {
            this.MemberId = userId;
            this.ApplicationId = ApplicationId;
            this.Review = review;
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