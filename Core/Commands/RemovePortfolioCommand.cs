using System.Collections.Generic;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands
{
    public class RemovePortfolioCommand : ICommand<int>
    {
        public int Id { get; protected set; }

        public RemovePortfolioCommand(int id)
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
