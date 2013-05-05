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
    public class TouchDetails
    {
        /// <summary>
        /// date of touch/click
        /// </summary>
        [JsonProperty(PropertyName = "d")]
        public DateTime Date { get; set; }

        /// <summary>
        /// coordinate X
        /// </summary>
        [JsonProperty(PropertyName = "cx")]
        public int ClientX { get; set; }

        /// <summary>
        /// Coordinate Y
        /// </summary>
        [JsonProperty(PropertyName = "cy")]
        public int ClientY { get; set; }

        /// <summary>
        /// pressure
        /// </summary>
        [JsonProperty(PropertyName = "p")]
        public int Press { get; set; }

        /// <summary>
        /// screen orientation
        /// </summary>
        [JsonProperty(PropertyName = "o")]
        public int Orientation { get; set; }
    }
}