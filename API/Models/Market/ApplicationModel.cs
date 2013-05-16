using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ApplicationModel : PaggingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }// Y: What is it? If it is link to package, we don't need, you have send id off application to get the package: http://appreadygo.com/application/{id}/package

        public string IconUri { get; set; }// Y: This url you can generate in android application by appid and static url: http://appreadygo.com/application/{id}/icon
        
        public bool HasIcon { get; set; }// Y: in case the app does not have any icon, you have to show some icon instead.

        public string ShortDescription { get; set; }// Y: What is it? we have just a description.

        public string Description { get; set; }
    }
}