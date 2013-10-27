using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class MembersPagingModel : StaffPagingModel
    {
        public new IEnumerable<MemberDetailsModel> Users { get; set; }

        public string CreateDateOrder { get; set; }

        public MembersPagingModel()
            : base(AdminMasterModel.MenuItem.Members)
        {
        }

        public MembersPagingModel(MenuItem menuItem)
            : base(menuItem)
        {
        }
    }
}