using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.Entities
{
    public enum AgeRange : short
    {
        None = 1,
        [AgeRangeDescription(DisplayDescription = "12/17", DropDownDescription = "12-17")]
        Range12_17 = 2,
        [AgeRangeDescription(DisplayDescription = "18/24", DropDownDescription = "18-24")]
        Range18_24 = 3,
        [AgeRangeDescription(DisplayDescription = "25/34", DropDownDescription = "25-34")]
        Range25_34 = 4,
        [AgeRangeDescription(DisplayDescription = "35/44", DropDownDescription = "35-44")]
        Range35_44 = 5,
        [AgeRangeDescription(DisplayDescription = "45/54", DropDownDescription = "45-54")]
        Range45_54 = 6,
        [AgeRangeDescription(DisplayDescription = "55/64", DropDownDescription = "55-64")]
        Range55_64 = 7,
        [AgeRangeDescription(DisplayDescription = "Abowe 65", DropDownDescription = "Abowe 65")]
        Abowe65 = 8
    }

    public class AgeRangeDescriptionAttribute : Attribute
    {
        public string DropDownDescription { get; set; }

        public string DisplayDescription { get; set; }
    }
}
