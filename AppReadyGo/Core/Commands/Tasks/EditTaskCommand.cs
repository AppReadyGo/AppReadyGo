using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.Commands.Tasks
{
    public class EditTaskCommand : ICommand<object>
    {
        public int Id { get; private set; }

        public int DescId { get; private set; }
 
        public int ApplicationId { get; private set; }

        public AgeRange? AgeRange { get; protected set; }

        public Gender? Gender { get; protected set; }

        public int? CountryId { get; set; }

        public string Zip { get; set; }

        public bool? Publish { get; set; }

        public int Audence { get; set; }

        public EditTaskCommand(int id, int descId, int applicationId, AgeRange? ageRange, Gender? gender, int? countryId, string zip, int audence, bool? publish)
        {
            this.Id = id;
            this.DescId = descId;
            this.ApplicationId = applicationId;
            this.AgeRange = ageRange;
            this.Gender = gender;
            this.CountryId = countryId;
            this.Zip = zip;
            this.Publish = publish;
            this.Audence = audence;
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
