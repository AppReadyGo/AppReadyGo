using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ApiMembersPagingModel : MembersPagingModel
    {
        public ApiMembersPagingModel()
            : base(AdminMasterModel.MenuItem.ApiMembers)
        {
        }
    }
}