﻿using System;
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

namespace AppReadyGo.API.Controllers
{
    public class MarketController : ApiController
    {
        // TODO: Yura: Why do we need the repositories? We already have DAL, this is just add additional layer that we don't need
        // In addition the approach to init objects, prevent test creating.
        static readonly IMarketAppsRepository mAppsRepository = new MarketAppsRepository();
        static readonly IMarketTestersRepository mTestersRepository = new MarketTestersRepository();

        [HttpPost]
        public bool Login(string email, string pass)
        {
            var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(email));
            if (securedDetails == null || securedDetails.Password != Encryption.SaltedHash(pass, securedDetails.PasswordSalt))
            {
                // "The user name or password provided is incorrect."
                return false;
            }
            else if (!securedDetails.Activated)
            {
                // "You account is not activated, please use the link from activation email to activate your account."
                return false;
            }
            return true;
        }


        // TODO: Yura: We do not need the last parameter, you will not call for the method if user did not accept terms and conditions.
        // TODO: Yura: Do you need methods that will return all countries and applic aion types (interests) that exists in system?
        // TODO: Yura: I think we also need method to update the details
        [HttpPost]
        public bool Register(string firstName, string lastName, string email, string pass, Gender gender, AgeRange ageRange, int contryId, string zip, int[] interest)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(pass))
            {
                return false;
            }

            var result = ObjectContainer.Instance.Dispatch(new CreateAPIMemberCommand(email, pass, firstName, lastName, gender, ageRange, contryId, zip, interest));

            if (!result.Validation.Any())
            {
                // TODO: Yura: do we need the activation email process. Are we sure that the email exists?
                //new MailGenerator(this.ControllerContext).Send(new ActivationEmail(model.Email));
                return true;
            }
            else
            {
                return false;
            }
        }


        [HttpGet]
        public IEnumerable<Application> GetApps(string email)
        {
            var applications = ObjectContainer.Instance.RunQuery(new GetApplicationsForUserQuery(email));
            return null;
        }

        // Yura: What is the method? Does the method return package? I need explanation about ranges.
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string filename)
        {
            //TODO: Add method to update downloaded count.
            //var result = ObjectContainer.Instance.Dispatch(new ApplicationDownloadedCommand(appId));
            
            string path = HttpContext.Current.Server.MapPath("~/" + filename);
            if (!File.Exists(path))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = File.Open(path, FileMode.Open);
                bool fullContent = true;
                if (this.Request.Headers.Range != null)
                {
                    fullContent = false;

                    // Currently we only support a single range.
                    RangeItemHeaderValue range = this.Request.Headers.Range.Ranges.First();


                    // From specified, so seek to the requested position.
                    if (range.From != null)
                    {
                        fileStream.Seek(range.From.Value, SeekOrigin.Begin);

                        // In this case, actually the complete file will be returned.
                        if (range.From == 0 && (range.To == null || range.To >= fileStream.Length))
                        {
                            fileStream.CopyTo(responseStream);
                            fullContent = true;
                        }
                    }
                    if (range.To != null)
                    {
                        // 10-20, return the range.
                        if (range.From != null)
                        {
                            long? rangeLength = range.To - range.From;
                            int length = (int)Math.Min(rangeLength.Value, fileStream.Length - range.From.Value);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                        // -20, return the bytes from beginning to the specified value.
                        else
                        {
                            int length = (int)Math.Min(range.To.Value, fileStream.Length);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                    }
                    // No Range.To
                    else
                    {
                        // 10-, return from the specified value to the end of file.
                        if (range.From != null)
                        {
                            if (range.From < fileStream.Length)
                            {
                                int length = (int)(fileStream.Length - range.From.Value);
                                byte[] buffer = new byte[length];
                                fileStream.Read(buffer, 0, length);
                                responseStream.Write(buffer, 0, length);
                            }
                        }
                    }
                }
                // No Range header. Return the complete file.
                else
                {
                    fileStream.CopyTo(responseStream);
                }
                fileStream.Close();
                responseStream.Position = 0;

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = fullContent ? HttpStatusCode.OK : HttpStatusCode.PartialContent;
                response.Content = new StreamContent(responseStream);
                return response;
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }



    }
}