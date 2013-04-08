using AppReadyGo.Core;
using AppReadyGo.Core.Logger;
using AppReadyGo.Model.Pages.Analytics;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Reflection;
using System.Web.Mvc;

namespace AppReadyGo.CustomModelBinders
{
    public class FilterParametersModelBinder : DefaultModelBinder
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var mState = bindingContext.ModelState;
            var queryString = controllerContext.HttpContext.Request.QueryString;
            try
            {
                return GetFilterModel<FilterParametersModel>(bindingContext.ModelState, queryString);
            }
            catch (Exception exp)
            {
                Guid guid = log.WriteError(exp, "Error to parse:{0}", queryString);
                mState.AddModelError("GeneralError", "Please contact to customer service: customerservice@mobillify.com, error guid:" + guid.ToString());
                return null;
            }
        }

        protected T GetFilterModel<T>(ModelStateDictionary mState, NameValueCollection queryString)
            where T : FilterParametersModel, new()
        {
            int applicationId = 0;
            var value = queryString["aid"];
            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out applicationId))
            {
                mState.Add("ApplicationId(aid)", new ModelState { });
                mState.AddModelError("ApplicationId(aid)", "Parameter ApplicationId is required.");
                return null;
            }

            var result = new T() { ApplicationId = applicationId };

            value = queryString["fd"];
            result.FromDate = string.IsNullOrEmpty(value) ? DateTime.UtcNow.AddDays(-30).StartDay() : DateTime.Parse(value).StartDay();
            value = queryString["td"];
            result.ToDate = string.IsNullOrEmpty(value) ? DateTime.UtcNow.EndDay() : DateTime.Parse(value).EndDay();
            value = queryString["ss"];
            if (string.IsNullOrEmpty(value))
            {
                result.ScreenSize = null;
            }
            else
            {
                var wh = value.Split(new char[] { 'X' });
                result.ScreenSize = new Size(int.Parse(wh[0]), int.Parse(wh[1]));
            }
            value = queryString["p"];
            result.Path = string.IsNullOrEmpty(value) ? null : value;
            value = queryString["l"];
            result.Language = string.IsNullOrEmpty(value) ? null : value;
            value = queryString["os"];
            result.OperationSystem = string.IsNullOrEmpty(value) ? null : value;
            value = queryString["ct"];
            result.City = string.IsNullOrEmpty(value) ? null : value;
            value = queryString["c"];
            result.Country = string.IsNullOrEmpty(value) ? null : value;
            return result;
        }
    }
}