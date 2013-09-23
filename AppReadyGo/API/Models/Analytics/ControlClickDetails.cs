using Newtonsoft.Json;
using System;


namespace AppReadyGo.API.Models.Analytics
{
    /// <summary>
    /// touch/click details iformation 
    /// </summary>
    /// <example>
    /// 
    /// </example>
    [JsonObject(MemberSerialization.OptOut)]
    public class ControlClickDetails
    {
        /// <summary>
        /// date of touch/click
        /// </summary>
        [JsonProperty(PropertyName = "dc")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Control Tag
        /// </summary>
        [JsonProperty(PropertyName = "tag")]
        public string ControlTag { get; set; }
    }
}