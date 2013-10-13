using System.Collections.Generic;
using AppReadyGo.Core.QueryResults.Application;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Web.Model.Pages.Application
{
    public class ApplicationIndexModel : AfterLoginMasterModel
    {
        public object ApplicationsData { get; set; }

        public IEnumerable<PublishItem> Publishes { get; set; }

        public ApplicationIndexModel(AfterLoginMasterModel.MenuItem selectedItem)
            : base(selectedItem)
        {
        }

        public class PublishItem
        {
            public int Id { get; set; }

            public string Description { get; set; }

            public string ApplicaionName { get; set; }

            public string Target { get; set; }

            public bool Alternate { get; set; }
        }
    }
}