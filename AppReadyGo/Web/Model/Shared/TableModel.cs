using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReadyGo.Model;

namespace AppReadyGo.Web.Model.Shared
{
    public class TableModel<TItem> : PagingModel
    {
        public string BaseUrlPart { get; set; }

        public IEnumerable<TItem> Items { get; set; }
    }
}