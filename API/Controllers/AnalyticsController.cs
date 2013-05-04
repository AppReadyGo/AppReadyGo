using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using AppReadyGo.API.Models;
using AppReadyGo.Common;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Core.Entities;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.Commands.Application;
using AppReadyGo.Core.Logger;
using System.Reflection;

namespace AppReadyGo.API.Controllers
{
    public class AnalyticsController : ApiController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public bool Index()
        {
            log.WriteInformation("Call to GetStatus");
            return true;
        }

        [HttpPost]
        public bool SubmitPackage([FromBody] FingerPrintData instance)
        {
            log.WriteInformation("Info: Status arrived with Data: " + instance.Value);

            try
            {
                if (null == instance || String.IsNullOrWhiteSpace(instance.Value))
                {
                    log.WriteWarning("SubmitPackage is null or the val is empty");
                    return false;
                }

                JsonPackage package = Deserialize<JsonPackage>(instance.Value);
                if (package == null)
                {
                    //Console.WriteLine("PROBLEMMMMMMMMM");
                    log.WriteError("SubmitPackage got smth that can't be deserialized");
                    //ApplicationLogging.WriteError(this.GetType(), "SubmitPackage : problem with JsonPackage");
                    return false;
                }

                PackageEvent objParserResult = EventParser.Parse(package) as PackageEvent;
                //objParserResult.Ip = instance.Ip;
                objParserResult.Ip = "Not needed";

                //---EventsServices objEventSvc = new EventsServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.EventsRepository");
                //---OperationResult objSaveResult = objEventSvc.HandlePackageEvent(objParserResult);
                //---log.WriteVerbose("return result is " + !objSaveResult.HasError);

                // Google analytics
                GooglePageView pageView = new GooglePageView("Get API status", "api.mobillify.com", "/ETService/GetStatus");
                TrackingRequest request = new RequestFactory().BuildRequest(pageView);
                GoogleTracking.FireTrackingEvent(request);


                //#region TEMP
                DataRepositoryServices objDataRepositorySvc = new DataRepositoryServices("EyeTracker.Domain", "EyeTracker.Domain.Repositories.DataRepository");
                return !objDataRepositorySvc.HandlePackageEvent(objParserResult).HasError;
                //#endregion



                //---return !objSaveResult.HasError;
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error in SubmitPackage");
                return false;
            }
        }

        /// <summary>
        /// JSON object deserialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_strObject"></param>
        /// <returns></returns>
        private static T Deserialize<T>(string p_strObject) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            byte[] bytes = Encoding.UTF8.GetBytes(p_strObject);
            MemoryStream ms = new MemoryStream(bytes);
            object obj = serializer.ReadObject(ms);

            return obj as T;
        }
    }
}
