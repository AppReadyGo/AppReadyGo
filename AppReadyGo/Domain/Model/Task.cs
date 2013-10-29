using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.Entities;
using AppReadyGo.Domain.Model.Users;

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

        public virtual int Audence { get; set; }

        public Task()
        {
        }

        public Task(TaskDescription description, Application application, AgeRange? ageRange, Gender? gender, Country country, string zip, int audence)
        {
            application.AddTask(this);
            this.Description = description;
            this.Application = application;
            this.AgeRange = ageRange;
            this.Gender = gender;
            this.Country = country;
            this.Zip = zip;
            this.CreatedDate = DateTime.Now;
            this.Audence = audence;
        }

        public virtual void Publish(bool publish = true)
        {
            this.PublishDate = publish ? (DateTime?)DateTime.UtcNow : null;
        }

        public virtual void Update(TaskDescription description, Application application, AgeRange? ageRange, Gender? gender, Country country, string zip, int audence)
        {
            if (this.Application != application)
            {
                this.Application.RemoveTask(this);
                application.AddTask(this);
            }
            this.Description = description;
            this.Application = application;
            this.AgeRange = ageRange;
            this.Gender = gender;
            this.Country = country;
            this.Zip = zip;
            this.CreatedDate = DateTime.Now;
            this.Audence = audence;
        }
    }
}
