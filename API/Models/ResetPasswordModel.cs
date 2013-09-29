using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models
{
    public class ResetPasswordModel
    {
        public string Key { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}