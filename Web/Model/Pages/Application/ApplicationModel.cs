using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppReadyGo.Model.Filter;
using AppReadyGo.Model.Master;
using System.Web.Mvc;

namespace AppReadyGo.Model.Pages.Application
{
    public class ApplicationModel : AfterLoginMasterModel
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Type")]
        public int Type { get; set; }

        public ApplicationViewModel ViewData { get; set; }

        public ApplicationModel()
            : base(AfterLoginMasterModel.MenuItem.Analytics)
        {
        }
    }
}