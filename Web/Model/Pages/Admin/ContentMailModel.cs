using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.Web.Model.Pages.Admin
{
    public class ContentMailModel : AdminMasterModel
    {
        public int Id { get; set; }

        public int Key { get; set; }

        public int SubKey { get; set; }

        public bool IsHTML { get; set; }

        public string Value { get; set; }

        public IEnumerable<SelectListItem> Keys { get; set; }

        public IEnumerable<SelectListItem> SubKeys { get; set; }

        public ContentMailModel()
            : base(MenuItem.ContentManager)
        {
        }
    }
}