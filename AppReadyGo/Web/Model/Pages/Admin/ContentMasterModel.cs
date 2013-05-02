
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ContentMasterModel : AdminMasterModel
    {
        public new MenuItem SelectedItem { get; set; }

        public ContentMasterModel(MenuItem selectedItem)
            : base(AdminMasterModel.MenuItem.ContentManager)
        {
            this.SelectedItem = selectedItem;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedItem ? "active" : string.Empty;
        }

        public new enum MenuItem
        {
            Items,
            Pages,
            Mails,
            None
        }
    }
}