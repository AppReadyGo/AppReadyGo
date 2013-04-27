using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Application
{
    public class PublishCommand : ICommand<int>
    {
        public int ApplicationId { get; private set; }

        public virtual AgeRange? AgeRange { get; protected set; }

        public virtual Gender? Gender { get; protected set; }

        public virtual int? CountryId { get; set; }

        public string Zip { get; set; }

        public PublishCommand(int applicationId, AgeRange? ageRange, Gender? gender, int? countryId, string zip)
        {
            this.ApplicationId = applicationId;
            this.AgeRange = ageRange;
            this.Gender = gender;
            this.CountryId = countryId;
            this.Zip = zip;
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
