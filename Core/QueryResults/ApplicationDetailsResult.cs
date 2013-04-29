﻿using System;
using AppReadyGo.Core.Entities;

namespace AppReadyGo.Core.QueryResults
{
    public class ApplicationDetailsResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Tuple<int, string> Type { get; set; }

        public string IconExt { get; set; }
    }
}