using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using AppReadyGo.Core.Entities;
using System.Web.Mvc;
using AppReadyGo.Core.QueryResults.Tasks;

namespace AppReadyGo.Common
{
    public static class Extentions
    {
        public static string GetStatus(this TaskDetailsResult task)
        {
            if (!task.PublishDate.HasValue)
            {
                return "Not Started";
            }
            else if (task.Installs == (int)task.Audence)
            {
                return "Complete";
            }
            else
            {
                return "In Progress";
            }
        }

        public static string GetTarget(this TaskDetailsResult task)
        {
            List<string> target = new List<string>();
            if (task.Gender.HasValue && task.Gender.Value != Gender.None)
            {
                target.Add(task.Gender.Value == Gender.Men ? "M" : "F");
            }
            if (task.AgeRange.HasValue && task.AgeRange.Value != AgeRange.None)
            {
                target.Add(task.AgeRange.Value.GetAgeRangeAttribute().DisplayDescription);
            }
            if (task.Country != null)
            {
                target.Add(task.Country.Item2);
            }

            return string.Join("-", target);
        }

        public static string GetContent(this ContentPredefinedKeys key)
        {
            switch (key)
            {
                case ContentPredefinedKeys.AndroidPackageVersion:
                    return ConfigurationManager.AppSettings["AndroidPackageVersion"];
                case ContentPredefinedKeys.ContentVersion:
                    return ConfigurationManager.AppSettings["ContentVersion"];
                default:
                    return string.Empty;
            }
        }

        public static string GetAppKey(this int applicationId)
        {
            return string.Format("MA-{0:000000}", applicationId);
        }

        public static string GetName(this AgeRange ageRange)
        {
            return ageRange.ToString();
        }

        public static IEnumerable<SelectListItem> Generate<T>(this IEnumerable<T> source, Func<T, SelectListItem> fun, string def = null, object defVal = null)
        {
            var lst = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(def))
            {
                lst.Add(new SelectListItem { Text = def, Value = defVal == null ? "" : defVal.ToString() });
            }

            lst.AddRange(source.Select(item => fun(item)).ToArray());
            if (!lst.Any(x => x.Selected))
            {
                lst.First().Selected = true;
            }
            return lst;
        }

        public static IEnumerable<T> GetList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static AgeRangeDescriptionAttribute GetAgeRangeAttribute(this AgeRange range)
        {
            var type = typeof(AgeRange);
            var memInfo = type.GetMember(range.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(AgeRangeDescriptionAttribute), false);
            return ((AgeRangeDescriptionAttribute)attributes[0]);
        }
    }
}