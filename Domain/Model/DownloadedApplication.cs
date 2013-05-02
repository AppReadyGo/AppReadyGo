using AppReadyGo.Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Domain.Model
{
    public class DownloadedApplication
    {
        public virtual int Id { get; protected set; }

        public virtual Application Application { get; protected set; }

        public virtual ApiMember User { get; protected set; }

        public DownloadedApplication()
        {
        }

        public DownloadedApplication(Application application, ApiMember user)
        {
            this.Application = application;
            this.User = user;
        }
    }
}
