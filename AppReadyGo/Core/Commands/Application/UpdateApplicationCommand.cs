using System.Collections.Generic;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Application
{
    public class UpdateApplicationCommand : ICommand<int>
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int TypeId { get; protected set; }

        public UpdateApplicationCommand(int id, string name, string description, int typeId)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.TypeId = typeId;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have Name parameter.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}
