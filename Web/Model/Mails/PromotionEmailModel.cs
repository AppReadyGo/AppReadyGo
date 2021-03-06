﻿using AppReadyGo.Web.Common.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Model.Mails
{
    public class PromotionEmailModel : MailMasterModel
    {
        public string ThisEmailUrl { get; set; }
        public string SiteRootUrl { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ContactUsEmail { get; set; }
        public string UnsubscribeUrl { get; set; }

        public PromotionEmailModel(bool isEmailProcess)
            : base(isEmailProcess)
        {
        }
    }
}