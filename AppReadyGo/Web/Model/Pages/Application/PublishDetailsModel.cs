using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Web.Model.Pages.Application
{
    public class PublishDetailsModel
    {
        public int Id { get; set; }

        public string Gender { get; set; }

        public string AgeRange { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string CreateDate { get; set; }
    }
}