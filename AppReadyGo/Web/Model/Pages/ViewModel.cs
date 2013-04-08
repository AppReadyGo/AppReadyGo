using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Model.Pages
{
    public class ViewModel<T> 
    {
        public T ViewData { get; set; }
    }
}