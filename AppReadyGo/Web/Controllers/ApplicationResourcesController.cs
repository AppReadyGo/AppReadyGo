using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppReadyGo.Web.Controllers
{
    public class ApplicationResourcesController : Controller
    {
        public FileContentResult Icon(int appId)
        {
            var dir = Server.MapPath("~/Content/Images/no_icon.png");
            if (System.IO.File.Exists(dir))
            {
                return new FileContentResult(System.IO.File.ReadAllBytes(dir), "image/png"); ;
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        public FileResult Package(int appId)
        {
            var dir = Server.MapPath("~/Content/Images/no_icon.png");
            if (System.IO.File.Exists(dir))
            {
                return new FileContentResult(System.IO.File.ReadAllBytes(dir), "image/png"); ;
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        public FileContentResult Screen(int appId, int id)
        {
            var dir = Server.MapPath("~/Content/Images/no_icon.png");
            if (System.IO.File.Exists(dir))
            {
                return new FileContentResult(System.IO.File.ReadAllBytes(dir), "image/png"); ;
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        public FileContentResult ScreenShot(int appId, int id)
        {
            var dir = Server.MapPath("~/Content/Images/no_icon.png");
            if (System.IO.File.Exists(dir))
            {
                return new FileContentResult(System.IO.File.ReadAllBytes(dir), "image/png"); ;
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }
    }
}
