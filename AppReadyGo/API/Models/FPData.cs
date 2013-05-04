using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AppReadyGo.API.Models
{
    public class FingerPrintData
    {
        [DataMember(Name="val")]
        public string Value 
        { 
            get; 
            set; 
        }

        public string Ip
        {
            get;
            set;
        }
    }
}
