using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ApplicationsPagingModel : AdminMasterModel, IPagingModel
    {
        public IEnumerable<ApplicationDetailsModel> Applications { get; set; }

        public string NameOrder { get; set; }

        public string TypeOrder { get; set; }

        public string CreateDateOrder { get; set; }

        public ApplicationsPagingModel()
            : base(MenuItem.Applications)
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