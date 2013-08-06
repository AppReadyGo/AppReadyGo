using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Model;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ContentManagerModel : ContentMasterModel
    {
        public ContentManagerModel()
            : base(MenuItem.Manager)
        {
        }
    }
}