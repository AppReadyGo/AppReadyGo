using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AppReadyGo.API.Models.Market
{
    public class PaggingModel<T>
    {
        [JsonProperty(PropertyName = "page")]
        public int CurPage { get; set; }

        [JsonProperty(PropertyName = "psize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int ItemsCount { get; set; }

        [JsonProperty(PropertyName = "items")]
        public T[] Collection { get; set; }
    }
}