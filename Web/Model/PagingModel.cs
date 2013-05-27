using AppReadyGo.Model.Master;
using AppReadyGo.Web.Model.Pages.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.Model
{
    public interface IPagingModel
    {
        bool IsOnePage { get; set; }

        int? PreviousPage { get; set; }

        int? NextPage { get; set; }

        int Count { get; set; }

        int TotalPages { get; set; }

        int CurPage { get; set; }

        string UrlPart { get; set; }

        string SearchStr { get; set; }

        string SearchStrUrlPart { get; set; }
    }

    public class PagingModel
    {
        public bool IsOnePage { get; set; }

        public int? PreviousPage { get; set; }

        public int? NextPage { get; set; }

        public int Count { get; set; }

        public int TotalPages { get; set; }

        public int CurPage { get; set; }

        public string UrlPart { get; set; }

        public string SearchStr { get; set; }

        public string SearchStrUrlPart { get; set; }
    }
}