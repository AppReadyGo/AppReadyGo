using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Model.Master
{
    public class BeforeLoginMasterModel : MainMasterModel
    {
        public MenuItem SelectedItem { get; private set; }

        public BeforeLoginMasterModel(MenuItem selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        public string GetMenuItemClass(MenuItem item)
        {
            return item == this.SelectedItem ? "current" : string.Empty;
        }

        public enum MenuItem
        {
            Home,
            HowItWorks,
            Tutorials,
            Products,
            PlanAndPricing,
            None
        }
    }
}