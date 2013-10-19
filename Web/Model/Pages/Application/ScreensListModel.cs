using AppReadyGo.Core.QueryResults.Applications;
using AppReadyGo.Model.Master;
using System.Collections.Generic;

namespace AppReadyGo.Model.Pages.Application
{
    public class ScreensListModel : AfterLoginMasterModel
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public IEnumerable<ScreenItemModel> Screens { get; set; }

        public string PathOrder { get; set; }

        public string WidthOrder { get; set; }

        public string HeightOrder { get; set; }

        public PagingModel Pagging { get; set; }

        public ScreensListModel()
            : base(MenuItem.Analytics)
        {
        }
    }

    public class ScreenItemModel : ScreenDataItemResult
    {
        public bool IsAlternative { get; set; }

        public string FileName { get; set; }
    }
}