using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.API
{
    public class ReviewApplicationCommand : ICommand<int>
    {
        public int MemberId { get; protected set; }

        public int TaskId { get; protected set; }

        public string Review { get; protected set; }

        public byte Rate { get; protected set; }

        public ReviewApplicationCommand(int userId, int taskId, string review, byte rate)
        {
            this.MemberId = userId;
            this.TaskId = taskId;
            this.Rate = rate;
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