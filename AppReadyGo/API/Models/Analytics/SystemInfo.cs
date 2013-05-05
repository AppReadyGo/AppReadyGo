using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AppReadyGo.API.Models.Analytics
{
    /// <summary>
    /// Represents System information about a mobile device 
    ///ssi //system info
    ///{
    ///     brn     //string - brand name
    ///     den     //string - device name
    ///     din     //string - display name
    ///     fin     //string - fingerprint name (not our; this is android property)
    ///     han     //string - hardware name
    ///     man     //string - manufacturer name
    ///     mon     //string - model name
    ///     opn     //string - operator name
    ///     prn     //string - product name
    ///     con     //string - The current development codename, or the string "REL" if this is a release build.
    ///     inc     //string - The internal value used by the underlying source control to represent this build.
    ///     rel     //string - The user-visible version string.
    ///     sdki    //integer - The user-visible SDK version of the framework; its possible values are defined in         //Build.VERSION_CODES.
    ///}
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class SystemInfo
    {
        [JsonProperty(PropertyName = "brn")]
        public string BrandName { get; set; }

        [JsonProperty(PropertyName = "den")]
        public string DeviceName { get; set; }

        [JsonProperty(PropertyName = "din")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "fin")]
        public string FingerprintName { get; set; }

        [JsonProperty(PropertyName = "han")]
        public string HardwareName { get; set; }

        [JsonProperty(PropertyName = "man")]
        public string ManufactureName { get; set; }

        [JsonProperty(PropertyName = "mon")]
        public string ModelName { get; set; }

        [JsonProperty(PropertyName = "opn")]
        public string OperatorName { get; set; }

        [JsonProperty(PropertyName = "prn")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "con")]
        public string DevCodeName { get; set; }
 
        [JsonProperty(PropertyName = "inc")]
        public string InternalName { get; set; }

        [JsonProperty(PropertyName = "rel")]
        public string RealVersionName { get; set; }

        [JsonProperty(PropertyName = "sdki")]
        public string SdkIdentName { get; set; }     
    }
}
