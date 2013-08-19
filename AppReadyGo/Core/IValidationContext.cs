using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core
{
    public interface IValidationContext
    {
        bool IsEmailExists(string email, int? userID = null);

        bool IsCorrectEmail(string email);

        bool IsCorrectPassword(string password);

        bool IsApplicationExists(int appId);
    }
}
