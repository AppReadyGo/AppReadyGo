using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppReadyGo.Model.Pages.Account
{
    public class ForgotPasswordModel : BeforeLoginMasterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        public ForgotPasswordModel()
            : base(BeforeLoginMasterModel.MenuItem.None)
        {
        }
    }
}