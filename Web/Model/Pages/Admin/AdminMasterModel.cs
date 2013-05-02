using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AppReadyGo.Model.Filter;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class AdminMasterModel : AfterLoginMasterModel
    {
        public new MenuItem SelectedItem { get; set; }

        public AdminMasterModel(MenuItem selectedItem)
            : base(AfterLoginMasterModel.MenuItem.Administrator)
        {
            this.SelectedItem = selectedItem;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedItem ? "active" : string.Empty;
        }

        public new enum MenuItem
        {
            Logs,
            Staff,
            Members,
            ApiMembers,
            ContentManager
        }
    }
}