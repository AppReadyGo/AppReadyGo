using AppReadyGo.Core.QueryResults.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Queries.Application
{
    public class GetApplicationsForUserQuery : IQuery<PagingResult<APIApplicationResult>>
    {
        public int UserId { get; private set; }
        public int CurPage { get; private set; }
        public int PageSize { get; private set; }

        public GetApplicationsForUserQuery(int userId, int curPage, int pageSize)
        {
            this.UserId = userId;
            this.CurPage = curPage;
            this.PageSize = pageSize;
        }
    }
}
