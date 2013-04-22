using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Model.Pages.Home
{

    public class ContentModel : BeforeLoginMasterModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public ContentModel(BeforeLoginMasterModel.MenuItem menuItem)
            : base(menuItem)
        {
        }
    }

    public class AuthenticatedContentModel : AfterLoginMasterModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public AuthenticatedContentModel(AfterLoginMasterModel.MenuItem menuItem)
            : base(menuItem)
        {
        }
    }

}
