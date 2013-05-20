using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UserResultModel
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "code")]
        public Result Code { get; set; }

        public enum Result
        {
            Successful = 0,
            WrongUserNamePassword,
            NotActivated
        }
    }
}