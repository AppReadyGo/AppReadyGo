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
            return false;
        }


        // TODO: Yura: We do not need the last parameter, you will not call for the method if user did not accept terms and conditions.
        [HttpPost]
        public bool Register(string name, string email, string pass, string gender, int age, string contry, string zip, int[] interest, bool terms)
        {
            return true;
        }


        [HttpGet]
        public IEnumerable<Application> GetApps(string email)
        {
            return null;
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri]string filename)
        {
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
