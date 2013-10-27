using System.Collections.Generic;
using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Model.Master;

namespace AppReadyGo.Web.Model.Pages.Application
{
    public class ApplicationIndexModel : AfterLoginMasterModel
    {
        public IEnumerable<TaskItem> Tasks { get; set; }

        public IEnumerable<ApplicationItem> Applications { get; set; }

        public ApplicationIndexModel()
            : base(AfterLoginMasterModel.MenuItem.Analytics)
        {
        }

        public class TaskItem
        {
            public int Id { get; set; }

            public string Description { get; set; }

            public int ApplicationId { get; set; }

            public string ApplicaionName { get; set; }

            public string Target { get; set; }

            public bool IsAlternative { get; set; }

            public object Installs { get; set; }

            public object Published { get; set; }

            public object Status { get; set; }

            public bool WasPublished { get; set; }
        }


        public class ApplicationItem
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public bool IsAlternative { get; set; }
        }
    }
}