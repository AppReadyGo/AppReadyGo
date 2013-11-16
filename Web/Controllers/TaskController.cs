using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Common;
using AppReadyGo.Core.Commands.Tasks;
using AppReadyGo.Core.Entities;
using AppReadyGo.Core.Queries.Analytics;
using AppReadyGo.Core.Queries.Application;
using AppReadyGo.Core.Queries.Tasks;
using AppReadyGo.Core.QueryResults.Tasks;
using AppReadyGo.Model.Pages.Application;
using Web.Common.Mails;

namespace AppReadyGo.Web.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult New()
        {
            var data = ObjectContainer.Instance.RunQuery(new GetTaskDataQuery());
            var model = new TaskModel
            {
                Applications = data.Applications.Generate(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }, "Please select application"),
                Countries = data.Countries.Generate(c => new SelectListItem { Text = c.Value, Value = c.Key.ToString() }, "Please select Country"),
                Genders = Extentions.GetList<Gender>().Generate(g => new SelectListItem { Text = g.ToString(), Value = g.ToString() }, "Please select Gender"),
                AgeRanges = Extentions.GetList<AgeRange>().Generate(a => new SelectListItem { Text = a.GetAgeRangeAttribute().DropDownDescription, Value = a.ToString() }, "Please select Age Range"),
                Descriptions = data.Descriptions.Generate(d => new SelectListItem { Text = d.Value, Value = d.Key.ToString() }, "Please select Description"),
                Audences = this.GetAudences().Generate(a => new SelectListItem { Text = a.ToString(), Value = a.ToString() }, "Please select Audence"),
            };
            return View("~/Views/Task/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult New(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                ObjectContainer.Instance.Dispatch(new AddTaskCommand(model.DescriptionId, model.ApplicationId, model.AgeRange, model.Gender, model.Country, model.Zip, model.Audence, model.Action == TaskModel.FormAction.Publish));
                return RedirectToAction("", "Application");
            }
            else
            {
                var data = ObjectContainer.Instance.RunQuery(new GetTaskDataQuery());
                model.Applications = data.Applications.Generate(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }, "Please select application");
                model.Countries = data.Countries.Generate(c => new SelectListItem { Text = c.Value, Value = c.Key.ToString() }, "Please select Country");
                model.Genders = Extentions.GetList<Gender>().Generate(g => new SelectListItem { Text = g.ToString(), Value = g.ToString() }, "Please select Gender");
                model.AgeRanges = Extentions.GetList<AgeRange>().Generate(a => new SelectListItem { Text = a.GetAgeRangeAttribute().DropDownDescription, Value = a.ToString() }, "Please select Age Range");
                model.Descriptions = data.Descriptions.Generate(d => new SelectListItem { Text = d.Value, Value = d.Key.ToString() }, "Please select Description");
                model.Audences = this.GetAudences().Generate(a => new SelectListItem { Text = a.ToString(), Value = a.ToString() }, "Please select Audence");
                return View("~/Views/Task/Edit.cshtml", model);
            }
        }

        public ActionResult Edit(int id)
        {
            var data = ObjectContainer.Instance.RunQuery(new GetTaskDataQuery(id));
            var model = new TaskModel
            {
                Id = id,
                ApplicationId = data.Task.ApplicationId,
                DescriptionId = data.Task.DescriptionId,
                AgeRange = data.Task.AgeRange,
                Gender = data.Task.Gender,
                Country = data.Task.Country.Item1,
                Zip = data.Task.Zip,
                Audence = data.Task.Audence,
                PublishDate = data.Task.PublishDate.HasValue ? data.Task.PublishDate.Value.ToShortDateString() : string.Empty,

                Applications = data.Applications.Generate(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }),
                Countries = data.Countries.Generate(c => new SelectListItem { Text = c.Value, Value = c.Key.ToString() }),
                Genders = Extentions.GetList<Gender>().Generate(g => new SelectListItem { Text = g.ToString(), Value = g.ToString() }),
                AgeRanges = Extentions.GetList<AgeRange>().Generate(a => new SelectListItem { Text = a.GetAgeRangeAttribute().DropDownDescription, Value = a.ToString() }),
                Descriptions = data.Descriptions.Generate(d => new SelectListItem { Text = d.Value, Value = d.Key.ToString() }),
                Audences = this.GetAudences().Generate(a => new SelectListItem { Text = a.ToString(), Value = a.ToString() }),
            };
            return View("~/Views/Task/Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Edit(TaskModel model)
        {
            if (ModelState.IsValid)
            {
                bool? publish = model.Action == TaskModel.FormAction.Save ? null : (bool?)(model.Action == TaskModel.FormAction.Publish);
                ObjectContainer.Instance.Dispatch(new EditTaskCommand(model.Id.Value, model.DescriptionId, model.ApplicationId, model.AgeRange, model.Gender, model.Country, model.Zip, model.Audence, publish));
                return RedirectToAction("", "Application");
            }
            else
            {
                var data = ObjectContainer.Instance.RunQuery(new GetTaskDataQuery());
                model.Applications = data.Applications.Generate(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
                model.Countries = data.Countries.Generate(c => new SelectListItem { Text = c.Value, Value = c.Key.ToString() });
                model.Genders = Extentions.GetList<Gender>().Generate(g => new SelectListItem { Text = g.ToString(), Value = g.ToString() });
                model.AgeRanges = Extentions.GetList<AgeRange>().Generate(a => new SelectListItem { Text = a.GetAgeRangeAttribute().DropDownDescription, Value = a.ToString() });
                model.Descriptions = data.Descriptions.Generate(d => new SelectListItem { Text = d.Value, Value = d.Key.ToString() });
                model.Audences = this.GetAudences().Generate(a => new SelectListItem { Text = a.ToString(), Value = a.ToString() });
                return View("~/Views/Task/Edit.cshtml", model);
            }
        }

        public IEnumerable<int> GetAudences()
        {
            return new int[]{ 5, 10, 20 };
        }

        public ActionResult Publish(int id)
        {
            var result = ObjectContainer.Instance.Dispatch(new PublishTaskCommand(id));
            if (!result.Validation.Any())
            {
                var data = ObjectContainer.Instance.RunQuery(new GetTaskDataQuery(id));
                new PublishEmail(data.Task).Send();
            }
           
            return RedirectToAction("", "Application");
        }

        public ActionResult Unpublish(int id)
        {
            ObjectContainer.Instance.Dispatch(new UnPublishTaskCommand(id));
            return RedirectToAction("", "Application");
        }
    }
}
