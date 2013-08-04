using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Admin
{
    public class PageDetailsResult
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int ItemsCount { get; set; }

        public string Theme { get; set; }
    }
}
