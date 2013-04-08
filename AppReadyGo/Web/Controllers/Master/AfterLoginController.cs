using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Model.Master;
using AppReadyGo.Model;
using AppReadyGo.Core;
using AppReadyGo.Common;

namespace AppReadyGo.Controllers.Master
{
    [Authorize]
    public abstract class AfterLoginController : Controller
    {
        public abstract AfterLoginMasterModel.MenuItem SelectedMenuItem { get; }

        protected virtual AfterLoginMasterModel GetModel(AfterLoginMasterModel.MenuItem selectedItem)
        {
            return new AfterLoginMasterModel(this, selectedItem);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, TViewModel>(GetModel(SelectedMenuItem), viewModel);

            return base.View(model);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AfterLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, TViewModel>(GetModel(selectedItem), viewModel);

            return base.View(model);
        }

        protected virtual ActionResult View<TViewModel>(string view, TViewModel viewModel, AfterLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, TViewModel>(GetModel(selectedItem), viewModel);

            return base.View(view, model);
        }

        protected virtual ActionResult View<TTemplateModel, TViewModel>(TTemplateModel templateModel, TViewModel viewModel)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, TTemplateModel, TViewModel>(GetModel(SelectedMenuItem), templateModel, viewModel);

            return base.View(model);
        }

        protected ActionResult View(string viewName, object model, string tmp)
        {
            return base.View(viewName, model);
        }
    }
}
