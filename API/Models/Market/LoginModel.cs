using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    [JsonObject(MemberSerialization.OptOut)]
    public class LoginModel
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "pass")]
        public string Password { get; set; }
    }
}