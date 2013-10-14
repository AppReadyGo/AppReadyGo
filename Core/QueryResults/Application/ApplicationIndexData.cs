using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class ApplicationIndexData
    {
        public PublishDetailsResult[] Tasks { get; set; }

        public ApplicationResult[] Applications { get; set; }
    }
}
