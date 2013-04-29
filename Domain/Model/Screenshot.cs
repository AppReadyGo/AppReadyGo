using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Domain.Model
{
    public class Screenshot
    {
        public virtual int Id { get; protected set; }

        public virtual Application Application { get; protected set; }

        public virtual string FileExtension { get; protected set; }

        public Screenshot()
        {
        }

        public Screenshot(Application application, string fileExtention)
        {
            application.AddScreenshot(this);
            this.Application = application;
            this.FileExtension = fileExtention;
        }
    }
}
