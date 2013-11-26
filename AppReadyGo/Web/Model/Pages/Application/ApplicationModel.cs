using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppReadyGo.Model.Filter;
using AppReadyGo.Model.Master;
using System.Web.Mvc;
using System.Collections.Generic;

namespace AppReadyGo.Model.Pages.Application
{
    public class ApplicationModel : AfterLoginMasterModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Type")]
        public int Type { get; set; }

        public string Content { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }

        public ApplicationModel()
            : base(AfterLoginMasterModel.MenuItem.Analytics)
        {
        }
    }
}