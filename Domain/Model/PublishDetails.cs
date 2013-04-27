using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Domain.Model
{
    public class PublishDetails
    {
        public virtual int Id { get; protected set; }

        public virtual AgeRange AgeRange { get; protected set; }

        public virtual Gender Gender { get; protected set; }

        public virtual Country Country { get; set; }

        public string Zip { get; set; }

        public DateTime CreatedDate { get; set; }

        public PublishDetails()
        {
        }

        public PublishDetails(AgeRange ageRange, Gender gender, Country country, string zip)
        {
            this.AgeRange = ageRange;
            this.Gender = gender;
            this.Country = country;
            this.Zip = zip;
            this.CreatedDate = DateTime.Now;
        }
    }
}
