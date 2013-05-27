using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class StaffPagingModel : AdminMasterModel, IPagingModel
    {
        public IEnumerable<StaffDetailsModel> Users { get; set; }

        public string EmailOrder { get; set; }

        public string NameOrder { get; set; }

        public StaffPagingModel()
            : base(AdminMasterModel.MenuItem.Staff)
        {
        }

        public StaffPagingModel(AdminMasterModel.MenuItem menuItem)
            : base(menuItem)
        {
        }

        #region IPagingModel Members

        public bool IsOnePage { get; set; }

        public int? PreviousPage { get; set; }

        public int? NextPage { get; set; }

        public int Count { get; set; }

        public int TotalPages { get; set; }

        public int CurPage { get; set; }

        public string UrlPart { get; set; }

        public string SearchStr { get; set; }

        public string SearchStrUrlPart { get; set; }

        #endregion
    }
}