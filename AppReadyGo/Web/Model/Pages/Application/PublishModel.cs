﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Master;
using System.Web.Mvc;

namespace AppReadyGo.Model.Pages.Application
{
    public class PublishModel : AfterLoginMasterModel
    {
        public int ApplicationId { get; set; }

        public string Target { get; set; }

        public IEnumerable<SelectListItem> TargetListItems { get; set; }

        public string FileName { get; set; }

        public PublishModel(MenuItem selectedItem)
            : base(selectedItem)
        {
        }
    }
}