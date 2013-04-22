using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Model.Pages.Account
{
    public class UnsubscribeModel : BeforeLoginMasterModel
    {
        public string Email { get; set; }

        public UnsubscribeModel()
            : base(BeforeLoginMasterModel.MenuItem.None)
        {
        }
    }
}