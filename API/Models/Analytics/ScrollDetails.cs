using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AppReadyGo.API.Models.Analytics
{
    /// <summary>
    /// scroll details infromation object 
    /// describes information about one scroll event - first touch : where a scroll begun 
    /// last touch : where a scroll ended
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ScrollDetails
    {
        /// <summary>
        /// std
        /// </summary>
        [JsonProperty(PropertyName = "std")]
        public TouchDetails StartTouchData { get; set; }

        /// <summary>
        /// ctd
        /// </summary>
        [JsonProperty(PropertyName = "ctd")]
        public TouchDetails CloseTouchData { get; set; }
    }
}



