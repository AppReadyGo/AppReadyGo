using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Application
{
    public class PublishesModel
    {
        public string ApplicationName { get; set; }

        public IEnumerable<PublishDetailsModel> Publishes { get; set; }
    }
}