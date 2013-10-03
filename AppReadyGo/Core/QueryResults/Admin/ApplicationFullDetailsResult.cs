using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Core.QueryResults.Users
{
    public class ApplicationFullDetailsResult : ApplicationDataItemResult
    {
        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public string PackageFileName { get; set; }

        public int Screenshots { get; set; }

        public int Screens { get; set; }
    }
}
