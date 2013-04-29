using AppReadyGo.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Application
{
    public class PublishesModel : AfterLoginMasterModel
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public IEnumerable<PublishDetailsModel> Publishes { get; set; }

        public PublishesModel()
            : base(MenuItem.Analytics)
        {
        }
    }
}