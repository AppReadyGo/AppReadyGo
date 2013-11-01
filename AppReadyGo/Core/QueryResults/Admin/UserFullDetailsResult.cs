using AppReadyGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AppReadyGo.Core.QueryResults.Users
{
    public class UserFullDetailsResult : UserDetailsResult
    {
        public int Id { get; set; }

        public bool Activated { get; set; }

        public bool SpecialAccess { get; set; }

        public DateTime? LastAccessDate { get; set; }

        public DateTime CreateDate { get; set; }


        public Gender? Gender { get; set; }
        public AgeRange? AgeRange { get; set; }
        
        public String CountryName { get; set; }

        public String getGenderDescription()
        {
                return Enum.GetName(typeof(Gender), (Gender ?? 0));
        }

        public String getAgeRangeDescription()
        {
            if (this.AgeRange.HasValue) /* && this.AgeRange.Value != AgeRange.None)*/{
                var type = typeof(AgeRange);
                var memInfo = type.GetMember(AgeRange.Value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(AgeRangeDescriptionAttribute), false);
                return ((AgeRangeDescriptionAttribute)attributes[0]).DisplayDescription;
            }
            else {
                return "None";
            }
        }
        
    
    }
}
