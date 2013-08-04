using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Model;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ContentPagesModel : ContentMasterModel
    {
        public string IdOrder { get; set; }

        public string UrlOrder { get; set; }
        
        public string SearchStrUrlPart { get; set; }

        public IEnumerable<ContentPagesItemModel> Pages { get; set; }

        public PagingModel Paging { get; set; }

        public ContentPagesModel()
            : base(MenuItem.Pages)
        {
        }

        public class ContentPagesItemModel
        {
            public int Id { get; set; }

            public string Url { get; set; }

            public string Theme { get; set; }

            public bool IsAlternative { get; set; }

            public int ItemsCount { get; set; }
        }

        public string ThemeOrder { get; set; }
    }
}