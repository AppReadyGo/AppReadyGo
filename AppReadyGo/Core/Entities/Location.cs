using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Entities
{
    public class Location
    {
        public virtual string Language { get; protected set; } 
        public virtual string Country { get; protected set; }
        public virtual string City { get; protected set; }

        public Location(string language, string country, string city)
        {
            this.Language = language;
            this.Country = country;
            this.City = city;
        }
    }
}
