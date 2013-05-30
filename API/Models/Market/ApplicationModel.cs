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
        public int Id { get; set; }

        public string Name { get; set; }

        //Url to application in market
        public string Url { get; set; }

        //public string IconUri { get; set; }// Y: This url you can generate in android application by appid and static url: http://appreadygo.com/application/{id}/icon
        // http://appreadygo.com/application/{id}/package
        
        public bool HasIcon { get; set; }// Y: in case the app does not have any icon, you have to show some icon instead.

        public string ShortDescription { get; set; }// Y: What is it? we have just a description.

        public string Description { get; set; }
    }
}