using AppReadyGo.API.Filters;
using AppReadyGo.API.Models.Market;
using AppReadyGo.Common;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands.Application;
using AppReadyGo.Core.Commands.Users;
using AppReadyGo.Core.Logger;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.Queries.Users;
using AppReadyGo.Web.Common.Mails;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using AppReadyGo.API.Common.Mails;

namespace AppReadyGo.API.Controllers
{
    [GoogleAnalyticsFilter]
    public class MarketController : ApiController
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public SettingsModel GetSettings()
        {
            return new SettingsModel
            {
                WebApplicationBaseURL = ConfigurationManager.AppSettings["WebApplicationBaseUrl"]
            };
        }

        [HttpPost]
        public UserResultModel Login([FromBody] LoginModel model)
        {

            if (model == null
                || String.IsNullOrWhiteSpace(model.Email)
                || String.IsNullOrWhiteSpace(model.Password))
            {
                return new UserResultModel { Code = UserResultModel.Result.WrongUserNamePassword };
            }

            // var body = HttpContext.Current.Request.Body();
            var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(model.Email));
            if (securedDetails == null || securedDetails.Password != Encryption.SaltedHash(model.Password, securedDetails.PasswordSalt))
            {
                // "The user name or password provided is incorrect."
                return new UserResultModel { Code = UserResultModel.Result.WrongUserNamePassword };
            }
            else if (!securedDetails.Activated)
            {
                // "You account is not activated, please use the link from activation email to activate your account."
                return new UserResultModel { Code = UserResultModel.Result.NotActivated };
            }
            return  new UserResultModel { Id = securedDetails.Id, Code = UserResultModel.Result.Successful };
        }

        [HttpPost]
        public bool Register([FromBody] UserModel model)
        {
            // var body = HttpContext.Current.Request.Body();
            if (string.IsNullOrEmpty(model.Email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                return false;
            }

            var result = ObjectContainer.Instance.Dispatch(new CreateAPIMemberCommand(model.Email, model.Password, model.FirstName, model.LastName, model.Gender, model.AgeRange, model.ContryId, model.Zip, model.Interests));

            if (!result.Validation.Any())
            {
                new APIActivationEmail(model.Email).Send();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <param name="gender"></param>
        /// <param name="ageRange"></param>
        /// <param name="contryId"></param>
        /// <param name="zip"></param>
        /// <param name="interest"></param>
        [HttpPost]
        public void UpdateUser([FromBody] UserModel model)
        {

            var result = ObjectContainer.Instance.Dispatch(new UpdateAPIMemberCommand(model.Id.Value, model.Email, model.Password, model.FirstName, model.LastName, model.Gender, model.AgeRange, model.ContryId, model.Zip, model.Interests));

            if (!result.Validation.Any())
            {
            }
        }


        /// <summary>
        /// Get all apps for this account 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public PaggingModel<ApplicationModel> GetApps(string email, int curPage, int pageSize)
        {
            var res = ObjectContainer.Instance.RunQuery(new GetApplicationsForUserQuery(email, curPage, pageSize));
            return new PaggingModel<ApplicationModel>
            {
                Collection = res.Collection.Select(a => new ApplicationModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    HasIcon = a.HasIcon
                })
                .ToArray(),
                CurPage = res.CurPage,
                PageSize = res.PageSize,
                ItemsCount = res.ItemsCount
            };
        }

        // Yura: What is the method? Does the method return package? I need explanation about ranges.
        //PM : Ranges allow you to download a big app package, this method is "Download App"
        // Yura: lets provide appid or something else, but not filename.
        [HttpGet]
        public HttpResponseMessage GetApp([FromUri]string filename)
        {
            //TODO: Add method to update downloaded count.
            int appId = 0;
            string email = string.Empty;
            var result = ObjectContainer.Instance.Dispatch(new ApplicationDownloadedCommand(email, appId));
            
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
                // Return filename
                //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                //{
                //    FileName = "foo.txt"
                //};
                return response;
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        // TODO: Yura: What does it mean?
        // PM : it means User (tester) has completed a task (used an app, reviewed it or something else)
        // Yura: Could you please explain me more about the functionality, we have to create a database and other functionality?
        /// <summary>
        /// TASK comple confirmation 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="task"></param>
        [HttpPost]
        public void TaskConfirm([FromBody] TaskModel model)
        { 
            
        }


    }

    public static class Ext
    {
        public static string Body(this HttpRequest request)
        {
            Stream req = request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            return new StreamReader(req).ReadToEnd();
        }
    }
}
