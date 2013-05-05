using Newtonsoft.Json;

namespace AppReadyGo.API.Models.Analytics
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FingerPrintData
    {
        [JsonProperty(PropertyName = "pack")]
        public Package Package 
        { 
            get; 
            set; 
        }

        [JsonProperty(PropertyName = "ip", Required = Required.Always)]
        public string Ip
        {
            get;
            set;
        }
    }
}
