using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class StaffDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public bool IsAlternative { get; set; }

        public string Roles { get; set; }

        public bool Activated { get; set; }

        public string Email { get; set; }

        public string LastAccess { get; set; }
    }
}