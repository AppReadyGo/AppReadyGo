using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Master;
using System.Web.Mvc;
using AppReadyGo.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AppReadyGo.Model.Pages.Application
{
    public class TaskModel : AfterLoginMasterModel
    {
        public int? Id { get; set; }

        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        [Display(Name = "Age Range")]
        public AgeRange? AgeRange { get; set; }

        public Gender? Gender { get; set; }

        public int? Country { get; set; }

        public string Zip { get; set; }

        public int DescriptionId { get; set; }

        public int Audence { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> AgeRanges { get; set; }

        public IEnumerable<SelectListItem> Descriptions { get; set; }

        public IEnumerable<SelectListItem> Applications { get; set; }

        public IEnumerable<SelectListItem> Audences { get; set; }

        public FormAction Action { get; set; }

        public TaskModel()
            : base(AfterLoginMasterModel.MenuItem.Analytics)
        {
        }

        public enum FormAction
        {
            Save,
            Publish,
            UnPublish
        }

        public string PublishDate { get; set; }
    }
}