using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Core.Entities;
using System.Web.Mvc;
using System.Web.Routing;
using System.Diagnostics;
using System.IO;

namespace AppReadyGo.API.Models.Market
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UserModel : ThirdPartyUserModel
    {
        [JsonProperty(PropertyName = "pass", Required = Required.Always)]
        public string Password { get; set; }
    }

    public class ThirdPartyUserModel
    {
        [JsonProperty(PropertyName = "id", Required = Required.Default)]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "email", Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public Gender? Gender { get; set; }

        [JsonProperty(PropertyName = "agerange")]
        public AgeRange? AgeRange { get; set; }

        [JsonProperty(PropertyName = "countryid")]
        public int? ContryId { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "interests")]
        public int[] Interests { get; set; }
    }
}