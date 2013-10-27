using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Commands.API
{
    public class TaskCompleteCommand : ICommand<int>
    {
        public int MemberId { get; protected set; }

        public int TaskId { get; protected set; }

        public TaskCompleteCommand(int userId, int taskId)
        {
            this.MemberId = userId;
            this.TaskId = taskId;
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