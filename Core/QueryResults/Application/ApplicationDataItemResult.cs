using System;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class ApplicationDataItemResult : ApplicationResult
    {
        public int Visits { get; set; }

        public string Type { get; set; }

        public bool IsActive { get; set; }
    }
}
