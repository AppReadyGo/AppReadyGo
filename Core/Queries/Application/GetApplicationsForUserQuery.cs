using AppReadyGo.Core.QueryResults.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetApplicationsForUserQuery : IQuery<IEnumerable<APIApplicationResult>>
    {
        public string Email { get; private set; }

        public GetApplicationsForUserQuery(string email)
        {
            this.Email = email;
        }
    }
}
