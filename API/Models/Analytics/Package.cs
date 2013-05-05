
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AppReadyGo.API.Models.Analytics
{
    /// <summary>
    /// Package data - contains data received from client 
    /// represents client parameters and contains information about using sessions
    /// </summary>
    /// <example>
    /// {
    ///     cid:'0001-1000',   //  - string - application ID 
    ///     sh:100,            //  - int- device  screen details????
    ///     sw:100,            //  - int- device screen details????
    ///     ssd:               //session data
    ///     [
    ///     si{}, si{}, si{}......
    ///     ],
    ///     ssi{}              //client system info
    /// }
    /// </example>
    [JsonObject(MemberSerialization.OptOut)]
    public class Package
    {
        [JsonProperty(PropertyName = "cid")]
        public string ClientKey { get; set; }

        [JsonProperty(PropertyName = "sh")]
        public int ScreenHeight { get; set; }

        [JsonProperty(PropertyName = "sw")]
        public int ScreenWidth { get; set; }

        [JsonProperty(PropertyName = "ssd")]
        public SessionInfo[] SessionsInfo { get; set; }

        [JsonProperty(PropertyName = "ssi")]
        public SystemInfo SystemInfo { get; set; }
    }

}