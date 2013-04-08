using AppReadyGo.Core.QueryResults.Application;
using System.Collections.Generic;

namespace AppReadyGo.Model.Pages.Application
{
    public class ScreensListModel : PagingModel
    {
        public int ApplicationId { get; set; }

        public string ApplicationDescription { get; set; }

        public IEnumerable<ScreenItemModel> Screens { get; set; }

        public string PathOrder { get; set; }

        public string WidthOrder { get; set; }

        public string HeightOrder { get; set; }
    }

    public class ScreenItemModel : ScreenDataItemResult
    {
        public bool IsAlternative { get; set; }

        public string FileExtention { get; set; }
    }
}