using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Analytics;
using System.Drawing;

namespace AppReadyGo.Core.Queries.Analytics
{
    public class HeatMapDataQuery : IQuery<HeatMapDataResult>
    {
        public int TaskId { get; set; }
        public string Path { get; private set; }
        public int? ScreenId { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        public HeatMapDataQuery(int taskId, string path, int? screenId = null, int? width = null, int? height = null)
        {
            this.TaskId = taskId;
            this.Path = path;
            this.ScreenId = screenId;
            this.Width = width;
            this.Height = height;
        }
    }
}
