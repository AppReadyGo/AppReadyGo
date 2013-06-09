using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Common;
using AppReadyGo.Core.Queries.Application;

namespace AppReadyGo.Web.Controllers
{
    public class ApplicationResourcesController : Controller
    {
        // http://appreadygo.com/application/{appId}/icon
        public FileContentResult Icon(int appId)
        {
            var appInfo = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(appId));
            if (!string.IsNullOrEmpty(appInfo.IconExt))
            {
                var dir = Server.MapPath(string.Format("~/Restricted/Icons/{0}{1}", appId, appInfo.IconExt));
                if (System.IO.File.Exists(dir))
                {
                    string contentType = string.Format("image/{0}", appInfo.IconExt.Substring(1));

                    return new FileContentResult(System.IO.File.ReadAllBytes(dir), contentType);
                }
            }
            throw new HttpException(404, "Not found");
        }

        // http://appreadygo.com/application/{appId}/package
        public FileResult Package(int appId)
        {
            var appInfo = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(appId));
            string packagePath = Server.MapPath(string.Format("~/Restricted/UserPackages/{0}", appId));

            if (System.IO.File.Exists(packagePath))
            {
                var contentType = Path.GetExtension(appInfo.PackageFileName) == ".jar" ? "application/java-archive" : "application/octet-stream";
                return base.File(packagePath, contentType, appInfo.PackageFileName);
            }
            else
            {
                throw new HttpException(404, "Not found");
            }
        }

        // http://appreadygo.com/application/{appId}/screenshot/{id}
        public FileContentResult ScreenShot(int appId, int id)
        {
            var appInfo = ObjectContainer.Instance.RunQuery(new GetApplicationDetailsQuery(appId));
            if (appInfo.Screenshots.ContainsKey(id))
            {
                var dir = Server.MapPath(string.Format("~/Restricted/Screenshots/{0}{1}", appId, appInfo.Screenshots[id]));
                if (System.IO.File.Exists(dir))
                {
                    string contentType = string.Format("image/{0}", appInfo.Screenshots[id].Substring(1));

                    return new FileContentResult(System.IO.File.ReadAllBytes(dir), contentType);
                }
            }
            throw new HttpException(404, "Not found");
        }
    }
}
