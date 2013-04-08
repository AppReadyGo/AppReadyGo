using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppReadyGo.Model.Pages.Portfolio
{
    public class PortfolioModel// : FilterModel
    {
        public virtual int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public virtual string Description { get; set; }

        [Required]
        [DisplayName("Time Zone")]
        public int TimeZone { get; set; }

        public SelectList ViewData { get; set; }
   }
}
