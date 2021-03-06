﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Commands.Users
{
    public class ResetPasswordCommand : ICommand<int?>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public ResetPasswordCommand(string email, string password)
        {
            this.Email = email;
            this.Password = password;
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
                yield return new ValidationResult(ErrorCode.EmailDoesNotExists, "Wrong UserName");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                yield return new ValidationResult(ErrorCode.WrongPassword, "The command must have an Password parameter.");
            }

            if (!validation.IsCorrectPassword(this.Password))
            {
                yield return new ValidationResult(ErrorCode.WrongPassword, "The password is wrong.");
            }
        }

        public IEnumerable<ValidationResult> ValidatePermissions(ISecurityContext security)
        {
            yield break;
        }
    }
}
