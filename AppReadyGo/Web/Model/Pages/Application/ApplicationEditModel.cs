using System.ComponentModel;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Model.Pages.Application
{
    public class ApplicationEditModel : ApplicationModel
    {
        [DisplayName("Type")]
        public new ApplicationType Type { get; set; }

        public int? SelectedApplicationId { get; set; }
    }
}