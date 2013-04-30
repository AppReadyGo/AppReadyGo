using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Model.Pages.Account
{
    public class ResetPasswordModel : BeforeLoginMasterModel
    {
        public string Key { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public ResetPasswordModel()
            : base(BeforeLoginMasterModel.MenuItem.None)
        {
        }
    }
}