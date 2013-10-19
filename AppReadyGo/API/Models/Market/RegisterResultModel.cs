using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AppReadyGo.API.Models.Market
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RegisterResultModel
    {

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "code")]
        public RegisterResult Code { get; set; }

        public enum RegisterResult
        {
            Successful = 0,
            MissingData,
            UserAlreadyRegistered
        }


        public bool AlreadyExists { get; set; }
    }
}