using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ApplicationModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        //Url to application in market
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        //public string IconUri { get; set; }// Y: This url you can generate in android application by appid and static url: http://appreadygo.com/application/{id}/icon
        [JsonProperty(PropertyName = "icon")]
        public bool HasIcon { get; set; }// Y: in case the app does not have any icon, you have to show some icon instead.

        [JsonProperty(PropertyName = "sdesc")]
        public string ShortDescription { get; set; }// Y: What is it? we have just a description.

        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "screens")]
        public int[] Screenshots { get; set; }
    }
}