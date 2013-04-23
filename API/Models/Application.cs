using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models
{
    public class Application
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string IconUri { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}