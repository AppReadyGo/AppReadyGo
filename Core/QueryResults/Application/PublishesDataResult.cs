using AppReadyGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class PublishesDataResult
    {
        public string ApplicationName { get; set; }

        public IEnumerable<PublishDetailsResult> PublishesDetails { get; set; }
    }
}
