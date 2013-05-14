using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.API.Models.Market
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RegisterModel
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "pass")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public Gender Gender { get; set; }

        [JsonProperty(PropertyName = "agerange")]
        public AgeRange AgeRange { get; set; }

        [JsonProperty(PropertyName = "countryid")]
        public int ContryId { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "interests")]
        public int[] Interests { get; set; }
    }
}