using AppReadyGo.API.Filters;
using AppReadyGo.API.Models.Market;
using AppReadyGo.Common;
using AppReadyGo.Core;
using AppReadyGo.Core.Commands.Applications;
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
using AppReadyGo.Core.Commands.API;

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
            var securedDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(model.Email, UserType.ApiMember));
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
        public RegisterResultModel ThirdPartyRegister([FromBody] ThirdPartyUserModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.MissingData };
            }

            var result = ObjectContainer.Instance.Dispatch(new CreateThirdPartyAPIMemberCommand(model.Email, model.FirstName, model.LastName, model.Gender, model.AgeRange, model.ContryId, model.Zip, model.Interests));

            if (!result.Validation.Any())
            {
                return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.Successful, Id = result.Result.Id, AlreadyExists = result.Result.AlreadyExists };
            }
            else
            {
                return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.MissingData };
            }
        }

        [HttpPost]
        public RegisterResultModel Register([FromBody] UserModel model)
        {
            log.WriteVerbose("--> Register");

            // var body = HttpContext.Current.Request.Body();
            if (string.IsNullOrEmpty(model.Email))
            {
                return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.MissingData } ;
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.MissingData } ;
            }

            log.WriteVerbose("Register: Dispatch CreateAPIMemberCommand command");
            var result = ObjectContainer.Instance.Dispatch(new CreateAPIMemberCommand(model.Email, model.Password, model.FirstName, model.LastName, model.Gender, model.AgeRange, model.ContryId, model.Zip, model.Interests));
            log.WriteVerbose("Register: CreateAPIMemberCommand command results:{0}", string.Join("; ", result.Validation.Select(x => x.Message).ToArray()));

            if (!result.Validation.Any())
            {
                new APIActivationEmail(model.Email).Send();
                return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.Successful, Id = result.Result } ;
            }
            else
            {
                if (result.Validation.Any(v => v.ErrorCode == ErrorCode.EmailExists))
                    return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.UserAlreadyRegistered };
                else
                    return new RegisterResultModel { Code = RegisterResultModel.RegisterResult.MissingData };
            }
        }

        [HttpPost]
        public ResultCode ForgotPassword([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return ResultCode.MissingData;
            }

            var userDetails = ObjectContainer.Instance.RunQuery(new GetUserSecuredDetailsByEmailQuery(email, UserType.ApiMember));
            if (userDetails != null)
            {
                if (userDetails.Type != UserType.ApiMember)
                {
                    return ResultCode.WrongEmail;
                }
                else
                {
                    new APIForgotPasswordMail(email).Send();
                }
            }
            else
            {
                ModelState.AddModelError("error", "The user does not exits in the system.");
            }

            return ResultCode.Successful;
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
        public ResultCode UpdateUser([FromBody] UserModel model)
        {

            var result = ObjectContainer.Instance.Dispatch(new UpdateAPIMemberCommand(model.Id.Value, model.Email, model.Password, model.FirstName, model.LastName, model.Gender, model.AgeRange, model.ContryId, model.Zip, model.Interests));

            if (!result.Validation.Any())
            {
                return ResultCode.MissingData;
            }
            else
            {
                return ResultCode.Successful;
            }
        }


        /// <summary>
        /// Get all apps for this account 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public PaggingModel<ApplicationModel> GetApps(int userId, int curPage, int pageSize)
        {
            var res = ObjectContainer.Instance.RunQuery(new GetApplicationsForUserQuery(userId, curPage, pageSize));
            return new PaggingModel<ApplicationModel>
            {
                Collection = res.Collection.Select(a => new ApplicationModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    HasIcon = a.HasIcon,
                    Url = a.Url,
                    Screenshots = a.Screenshots.Keys.ToArray()
                })
                .ToArray(),
                CurPage = res.CurPage,
                PageSize = res.PageSize,
                ItemsCount = res.ItemsCount
            };
        }

        [HttpGet]
        public HttpResponseMessage GetApp(int userId, int appId)
        {
            var appInfo = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(appId));
            var webPath = ConfigurationManager.AppSettings["WebApplicationPath"];
            if (webPath.Contains("~/"))
            {
                webPath = webPath.Remove(0, 2).Replace("/", "\\");
                webPath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), webPath);
            }
            var userPackagePath = string.Format("Restricted\\UserPackages\\{0}", appId);
            string packagePath = Path.Combine(webPath, userPackagePath);

            //var res = ObjectContainer.Instance.RunQuery(new GetAppForUserQuery(userId, appId));

            string filename = string.Empty;
            string path = HttpContext.Current.Server.MapPath("~/" + filename);

            if (!System.IO.File.Exists(packagePath))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = File.Open(packagePath, FileMode.Open);
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
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(Path.GetExtension(appInfo.PackageFileName) == ".jar" ? "application/java-archive" : "application/octet-stream");
                // Return filename
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = appInfo.PackageFileName
                };

                ObjectContainer.Instance.Dispatch(new ApplicationDownloadedCommand(userId, appId));
                return response;
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// TASK comple confirmation 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="task"></param>
        //[HttpPost]
        //public void TaskConfirm([FromBody] TaskModel model)
        //{ 
            
        //}

        [HttpPost]
        public void Used([FromBody] TaskModel model)
        {
            ObjectContainer.Instance.Dispatch(new ApplicationUsedCommand(model.UserId, model.AppId));
        }

        [HttpPost]
        public void UpdateReview([FromBody] ReviewModel model)
        {
            ObjectContainer.Instance.Dispatch(new ApplicationUpdateReviewCommand(model.UserId, model.AppId, model.Review));
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
