using System.Collections.Generic;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Application
{
    public class CreateApplicationCommand : ICommand<int>
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Type { get; protected set; }
        public string IconExt { get; protected set; }

        public CreateApplicationCommand(string name, string description, int type, string iconExt)
        {
            this.Name = name;
            this.Description = description;
            this.Type = type;
            this.IconExt = iconExt;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "Command must have Name parameter.");
            }

            if (!string.IsNullOrWhiteSpace(this.Description) && this.Description.Length > 500)
            {
                yield return new ValidationResult(ErrorCode.WrongDescription, "The description size is wrong.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}
