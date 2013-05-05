using System;
using System.Runtime.Serialization;

namespace AppReadyGo.API.Models.Analytics
{
    /// <summary>
    /// view area iformation object
    /// </summary>
    [DataContract]
    public class ViewAreaDetails
    {
        [DataMember(Name = "cx")]
        public int CoordX { get; set; }

        [DataMember(Name = "cy")]
        public int CoordY { get; set; }

        [DataMember(Name="sd")]
        public DateTime StartDate { get; set; }

        [DataMember(Name="fd")]
        public DateTime FinishDate { get; set; }

        [DataMember(Name="o")]
        public int Orientation { get; set; }
    }
}