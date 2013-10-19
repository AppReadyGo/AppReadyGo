using System;
using System.Collections.Generic;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Applications
{
    public class RemoveApplicationCommand : ICommand<RemoveApplicationCommandResult>
    {
        public int Id { get; protected set; }

        public RemoveApplicationCommand(int id)
        {
            this.Id = id;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (!validation.IsApplicationExists(this.Id))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, string.Format("The application {0} does not exists", this.Id));
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }

    }

    public class RemoveApplicationCommandResult
    {
        public int AppId { get; set; }

        public IEnumerable<Tuple<int, string>> Screens { get; set; }

        public IEnumerable<Tuple<int, string>> Screenshots { get; set; }

        public string IconExt { get; set; }
    }
}
