using AppReadyGo.Core.Logger;
using AppReadyGo.Model.Pages.Analytics;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace AppReadyGo.CustomModelBinders
{

    /// <summary>
    /// 
    /// </summary>
    public class ABFilterParametersModelBinder : FilterParametersModelBinder
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var mState = bindingContext.ModelState;
            var queryString = controllerContext.HttpContext.Request.QueryString;
            try
            {
                var result = GetFilterModel<ABFilterParametersModel>(mState, queryString);
                string value = queryString["sp"];
                result.SecondPath = string.IsNullOrEmpty(value) ? queryString["p"] : value;
                return result;
            }
            catch (Exception exp)
            {
                Guid guid = log.WriteError(exp, "Error to parse:{0}", queryString);
                mState.AddModelError("GeneralError", "Please contact to customer service: customerservice@mobillify.com, error guid:" + guid.ToString());
                return null;
            }
        }
    }
}