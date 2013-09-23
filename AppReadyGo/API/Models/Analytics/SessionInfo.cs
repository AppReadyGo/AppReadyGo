using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace AppReadyGo.API.Models.Analytics
{
    /// <summary>
    /// Session Info object 
    /// contains infromation about one single using session 
    /// </summary>
    /// <example>
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </example>
    [JsonObject(MemberSerialization.OptOut)]
    public class SessionInfo
    {

        /// <summary>
        /// uri
        /// </summary>
        [JsonProperty(PropertyName = "uri")]
        public string PageUri { get; set; }

        /// <summary>
        /// cw
        /// </summary>
        [JsonProperty(PropertyName = "cw")]
        public int ClientWidth { get; set; }

        /// <summary>
        /// ch
        /// </summary>
        [JsonProperty(PropertyName = "ch")]
        public int ClientHeight { get; set; }

        /// <summary>
        /// ss
        /// </summary>
        [JsonProperty(PropertyName = "ss")]
        public DateTime SessionStartDate { get; set; }

        /// <summary>
        /// sc
        /// </summary>
        [JsonProperty(PropertyName = "sc")]
        public DateTime SessionCloseDate { get; set; }

        /// <summary>
        /// tda
        /// </summary>
        [JsonProperty(PropertyName = "tda")]
        public TouchDetails[] TouchDetails { get; set; }

        /// <summary>
        /// sda
        /// </summary>
        [JsonProperty(PropertyName = "sda")]
        public ScrollDetails[] ScrollDetails { get; set; }

        /// <summary>
        /// vwa
        /// </summary>
        [JsonProperty(PropertyName = "vwa")]
        public ViewAreaDetails[] ViewAreaDetails { get; set; }

        /// <summary>
        /// vwa
        /// </summary>
        [JsonProperty(PropertyName = "clks")]
        public ControlClickDetails[] ControlClickDetails { get; set; }
    }
}