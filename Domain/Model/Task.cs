using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Domain.Model
{
    public class Task
    {
        public virtual int Id { get; protected set; }

        public virtual TaskDescription Description { get; protected set; }

        public virtual Application Application { get; protected set; }

        public virtual AgeRange? AgeRange { get; protected set; }

        public virtual Gender? Gender { get; protected set; }

        public virtual Country Country { get; set; }

        public virtual string Zip { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime? PublishDate { get; set; }

        public Task()
        {
        }

        public Task(TaskDescription description, Application app, AgeRange? ageRange, Gender? gender, Country country, string zip)
        {
            app.AddTask(this);
            this.Description = description;
            this.Application = app;
            this.AgeRange = ageRange;
            this.Gender = gender;
            this.Country = country;
            this.Zip = zip;
            this.CreatedDate = DateTime.Now;
        }

        public virtual void Publish(bool publish = true)
        {
            this.PublishDate = publish ? (DateTime?)DateTime.UtcNow : null;
        }
    }
}
