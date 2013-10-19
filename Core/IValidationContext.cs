using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core
{
    public interface IValidationContext
    {
        bool IsEmailExists(string email, int? userID = null, params UserType[] userTypes);

        bool IsCorrectEmail(string email);

        bool IsCorrectPassword(string password);

        bool IsApplicationExists(int appId);

        bool IsCurrentUserApplicationExists(int appId);
    }
}
