using System.Collections.Generic;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Application
{
    public class RemoveApplicationCommand : ICommand<int>
    {
        public int Id { get; protected set; }

        public RemoveApplicationCommand(int id)
        {
            this.Id = id;
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
