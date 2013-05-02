using AppReadyGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ContentItemsModel : ContentMasterModel
    {
        public string IdOrder { get; set; }

        public string UrlOrder { get; set; }
        
        public string SearchStrUrlPart { get; set; }

        public IEnumerable<ContentItemsKeyModel> Keys { get; set; }

        public PagingModel Paging { get; set; }

        public ContentItemsModel()
            : base(MenuItem.Items)
        {
        }

        public class ContentItemsKeyModel
        {
            public int Id { get; set; }

            public string Url { get; set; }

            public bool IsAlternative { get; set; }

            public int ItemsCount { get; set; }
        }
    }
}