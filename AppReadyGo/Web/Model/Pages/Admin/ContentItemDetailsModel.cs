using AppReadyGo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ContentItemDetailsModel : ContentMasterModel
    {
        public int ParentId { get; set; }

        public string ParentUrl { get; set; }

        public int ItemType { get; set; }

        public int Id { get; set; }

        public string SubKey { get; set; }

        public bool IsHtml { get; set; }

        public string Value { get; set; }

        public ContentItemDetailsModel()
            : base(MenuItem.Items)
        {
        }

    }
}