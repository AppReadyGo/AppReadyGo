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
using AppReadyGo.API.Models.Analytics;
using AppReadyGo.Core.Commands.API;
using System.Configuration;
using GoogleAnalyticsDotNet.Common.Data;
using GoogleAnalyticsDotNet.Common.Helpers;
using GoogleAnalyticsDotNet.Common;

namespace AppReadyGo.API.Controllers
{
    public class AnalyticsController : ApiController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);
        private static bool googleAnalytics = bool.Parse(ConfigurationManager.AppSettings["GoogleAnalytics"]);
        [HttpGet]
        public bool Index()
        {
            log.WriteInformation("Call to GetStatus");
            return true;
        }

        [HttpPost]
        public bool SubmitPackage([FromBody] FingerPrintData data)
        {
            try
            {
                if (googleAnalytics)
                {
                    // Google analytics
                    GooglePageView pageView = new GooglePageView("Submit Package", "api.appreadygo.com", "/Analytics/SubmitPackage");
                    TrackingRequest request = new RequestFactory().BuildRequest(pageView);
                    GoogleTracking.FireTrackingEvent(request);
                }

                if (data == null || data.Package == null)
                {
                    log.WriteError("Error to submit package the parameter is null");
                    return false;
                }

                int appId = 0;
                if (string.IsNullOrEmpty(data.Package.ClientKey) || data.Package.ClientKey.Length < 5 || !int.TryParse(data.Package.ClientKey.Split(new char[] { '-' })[2], out appId))
                {
                    log.WriteError("Error to submit package the ClientKey:{0} is wrong", data.Package.ClientKey);
                    return false;
                }

                Location location = null;// TODO: get the location by ip
                // We have to create some queue to hold the ips and process the queue by other service.
                // However, all the logic have to write to some queue and not to strate to database.

                var res = ObjectContainer.Instance.Dispatch(new AddPackageCommand(
                    appId,
                    location,
                    data.Package.SystemInfo.RealVersionName,
                    null,
                    data.Package.ScreenWidth,
                    data.Package.ScreenHeight,
                    new AppReadyGo.Core.Entities.SystemInfo
                    {
                    },
                    data.Package.SessionsInfo.Select(s => new AddPackageCommand.Session
                    {
                        ClientHeight = s.ClientHeight,
                        ClientWidth = s.ClientWidth,
                        Path = s.PageUri,
                        CloseDate = s.SessionCloseDate,
                        StartDate = s.SessionStartDate,
                        ScreenViewParts = s.ViewAreaDetails.Select(v => new AddPackageCommand.ViewPart
                        {
                            FinishDate = v.FinishDate,
                            StartDate = v.StartDate,
                            Orientation = v.Orientation,
                            ScrollLeft = v.CoordX,
                            ScrollTop = v.CoordY
                        }),
                        Scrolls = s.ScrollDetails.Select(scroll => new AddPackageCommand.Scroll
                        {
                            FirstTouch = new AddPackageCommand.Click
                            {
                                ClientX = scroll.StartTouchData.ClientX,
                                ClientY = scroll.StartTouchData.ClientY,
                                Date = scroll.StartTouchData.Date,
                                Orientation = scroll.StartTouchData.Orientation,
                                Press = scroll.StartTouchData.Press
                            },
                            LastTouch = new AddPackageCommand.Click
                            {
                                ClientX = scroll.CloseTouchData.ClientX,
                                ClientY = scroll.CloseTouchData.ClientY,
                                Date = scroll.CloseTouchData.Date,
                                Orientation = scroll.CloseTouchData.Orientation,
                                Press = scroll.CloseTouchData.Press
                            }
                        }),
                        Clicks = s.TouchDetails.Select(t => new AddPackageCommand.Click
                        {
                            ClientX = t.ClientX,
                            ClientY = t.ClientY,
                            Date = t.Date,
                            Orientation = t.Orientation,
                            Press = t.Press
                        })
                    })));

                return !res.Validation.Any();
            }
            catch (Exception ex)
            {
                log.WriteError(ex, "Error to submit package for application:{0}", data.Package.ClientKey);
                return false;
            }
        }
    }
}
