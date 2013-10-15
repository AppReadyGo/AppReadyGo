using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model.Master;
using System.Web.Mvc;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Model.Pages.Application
{
    public class TaskModel : AfterLoginMasterModel
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public AgeRange? AgeRange { get; set; }

        public IEnumerable<SelectListItem> AgeRanges { get; set; }

        public Gender? Gender { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public int? Country { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public string Zip { get; set; }

        public TaskModel()
            : base(AfterLoginMasterModel.MenuItem.Analytics)
        {
        }
    }
}