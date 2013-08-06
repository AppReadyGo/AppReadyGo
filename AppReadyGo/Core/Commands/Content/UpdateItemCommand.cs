using System;
using System.Collections.Generic;
using System.Linq;

namespace AppReadyGo.Core.Commands.Content
{
    public class UpdateItemCommand : ICommand<int>
    {
        public int Id { get; private set; }

        public string Value { get; private set; }

        public UpdateItemCommand(int id, string value)
        {
            this.Id = id;
            this.Value = value;
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
