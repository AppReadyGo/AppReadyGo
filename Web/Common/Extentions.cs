using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Common
{
    public static class Extentions
    {
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
        //public static string GetContentUrl(this Url

        public static string GetName(this AgeRange ageRange)
        {
            return ageRange.ToString();
        }
    }
}