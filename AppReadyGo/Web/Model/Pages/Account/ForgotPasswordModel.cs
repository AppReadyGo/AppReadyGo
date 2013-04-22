using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Model.Pages.Account
{
    public class ForgotPasswordModel : BeforeLoginMasterModel
    {
        public string Email { get; set; }

        public ForgotPasswordModel()
            : base(BeforeLoginMasterModel.MenuItem.None)
        {
        }
    }
}