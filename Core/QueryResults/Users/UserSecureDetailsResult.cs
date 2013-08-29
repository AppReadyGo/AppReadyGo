using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Users
{
    public class UserSecuredDetailsResult
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public bool Activated { get; set; }

        public int Id { get; set; }

        public bool SpecialAccess { get; set; }

        public IEnumerable<StaffRole> Roles { get; set; }

        public bool AcceptedTermsAndConditions { get; set; }

        public UserType Type { get; set; }
    }
}
