using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ApplicationDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public string PackageFileName { get; set; }

        public int Screenshots { get; set; }

        public int Screens { get; set; }

        public int Visits { get; set; }

        public string Type { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public string IconExt { get; set; }

        public bool Published { get; set; }

        public int Downloaded { get; set; }

        public string CreateDate { get; set; }

        public int Index { get; set; }

        public bool IsAlternative { get; set; }
    }
}