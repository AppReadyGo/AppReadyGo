using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Task
{
    public class AddTaskCommand : ICommand<int>
    {
        public int DescId { get; private set; }
 
        public int ApplicationId { get; private set; }

        public virtual AgeRange? AgeRange { get; protected set; }

        public virtual Gender? Gender { get; protected set; }

        public virtual int? CountryId { get; set; }

        public string Zip { get; set; }
 
        public bool Publish { get; set; }

        public AddTaskCommand(int descId, int applicationId, AgeRange? ageRange, Gender? gender, int? countryId, string zip, bool publish)
        {
            this.DescId = descId;
            this.ApplicationId = applicationId;
            this.AgeRange = ageRange;
            this.Gender = gender;
            this.CountryId = countryId;
            this.Zip = zip;
            this.Publish = publish;
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
