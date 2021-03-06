﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Commands.Users
{
    public class UnsubscribeCommand : ICommand<bool>
    {
        public string Email { get; set; }

        public UnsubscribeCommand(string email)
        {
            this.Email = email;
        }

        public IEnumerable<ValidationResult> Validate(IValidationContext validation)
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                yield return new ValidationResult(ErrorCode.WrongParameter, "UserName is required parameter");
            }

            if (!validation.IsCorrectEmail(this.Email))
            {
                yield return new ValidationResult(ErrorCode.WrongEmail, "Wrong UserName");
            }

            if (!validation.IsEmailExists(this.Email))
            {
                yield return new ValidationResult(ErrorCode.EmailExists, "Wrong UserName");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}
