using System.ComponentModel;
using AppReadyGo.Core.Entities;
using System.Collections.Generic;
using System;
using AppReadyGo.Core.QueryResults.Application;

namespace AppReadyGo.Model.Pages.Application
{
    public class ApplicationEditModel : ApplicationModel
    {
        public string IconPath { get; set; }

        public IEnumerable<string> ScreensPathes { get; set; }

        public IEnumerable<PublishDetailsResult> Publishes { get; set; }
    }
}