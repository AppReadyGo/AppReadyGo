using AppReadyGo.Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Domain.Model
{
    public class APIMemberApplication
    {
        public virtual int Id { get; protected set; }

        public virtual Application Application { get; protected set; }

        public virtual ApiMember User { get; protected set; }

        public virtual bool Used { get; protected set; }

        public virtual string Review { get; protected set; }

        public APIMemberApplication()
        {
        }

        public APIMemberApplication(Application application, ApiMember user)
        {
            this.Application = application;
            this.User = user;
        }

        public virtual void ApplicationWasUsed()
        {
            this.Used = true;
        }

        public virtual void UpdateReview(string review)
        {
            this.Review = review;
        }
    }
}
