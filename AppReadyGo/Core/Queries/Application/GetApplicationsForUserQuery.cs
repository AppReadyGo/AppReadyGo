using AppReadyGo.Core.QueryResults.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetApplicationsForUserQuery : IQuery<PagingResult<APIApplicationResult>>
    {
        public string Email { get; private set; }
        public int CurPage { get; private set; }
        public int PageSize { get; private set; }

        public GetApplicationsForUserQuery(string email, int curPage, int pageSize)
        {
            this.Email = email;
            this.CurPage = curPage;
            this.PageSize = pageSize;
        }
    }
}
