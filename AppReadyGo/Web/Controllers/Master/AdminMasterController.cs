using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppReadyGo.Model.Master;
using AppReadyGo.Model;
using AppReadyGo.Model.Pages.Admin;

namespace AppReadyGo.Controllers.Master
{
    public class AdminMasterController : AfterLoginController
    {
        public override Model.Master.AfterLoginMasterModel.MenuItem SelectedMenuItem
        {
            get { return Model.Master.AfterLoginMasterModel.MenuItem.Administrator; }
        }
        
        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AdminMasterModel.MenuItem leftMenuSelectedItem, AfterLoginMasterModel.MenuItem selectedItem)
        {
            var model = new ViewModelWrapper<AfterLoginMasterModel, AdminMasterModel, TViewModel>(GetModel(selectedItem), new AdminMasterModel(leftMenuSelectedItem), viewModel);

            return base.View(model);
        }

        protected virtual ActionResult View<TViewModel>(TViewModel viewModel, AdminMasterModel.MenuItem leftMenuSelectedItem)
        {
            return View(new AdminMasterModel(leftMenuSelectedItem), viewModel);
        }
    }
}
