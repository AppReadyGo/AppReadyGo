﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppReadyGo.Core.QueryResults.Analytics;
using System.Drawing;

namespace AppReadyGo.Core.Queries.Analytics
{
    public class HeatMapDataQuery : IQuery<HeatMapDataResult>
    {
        public long AplicationId { get; private set; }
        public string Path { get; private set; }
        public Size ScreenSize { get; private set; }
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }

        public HeatMapDataQuery(long appId, string path, Size screenSize, DateTime fromDate, DateTime toDate)
        {
            this.AplicationId = appId;
            this.Path = path;
            this.ScreenSize = screenSize;
            this.FromDate = fromDate.StartDay();
            this.ToDate = toDate.EndDay();
        }
    }
}