using AppReadyGo.Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Domain.Model
{
    public class APIMemberTask
    {
        public virtual int Id { get; protected set; }

        public virtual ApiMember User { get; protected set; }

        public virtual Task Task { get; protected set; }

        public virtual string Review { get; protected set; }

        public virtual byte Rate { get; protected set; }

        public APIMemberTask()
        {
        }

        public APIMemberTask(Task task, ApiMember user)
        {
            this.Task = task;
            this.User = user;
        }

        public virtual void UpdateRate(byte rate)
        {
            this.Rate = rate;
        }

        public virtual void UpdateReview(string review)
        {
            this.Review = review;
        }
    }
}
