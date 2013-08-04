using AppReadyGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ContentItemModel : ContentMasterModel
    {
        public int KeyId { get; set; }

        public string KeyUrl { get; set; }

        public int ItemType { get; set; }

        public string IdOrder { get; set; }

        public string IsHtmlOrder { get; set; }

        public string SubKeyOrder { get; set; }
        
        public string SearchStrUrlPart { get; set; }

        public IEnumerable<ContentItemsItemModel> Items { get; set; }

        public PagingModel Paging { get; set; }

        public ContentItemModel()
            : base(MenuItem.Items)
        {
        }

        public class ContentItemsItemModel
        {
            public int Id { get; set; }

            public string SubKey { get; set; }

            public bool IsHtml { get; set; }

            public bool IsAlternative { get; set; }
        }
    }
}