﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.QueryResults.Applications
{
    public class ScreenDetailsDataResult : ScreenDataResult
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string FileExtention { get; set; }
    }
}
