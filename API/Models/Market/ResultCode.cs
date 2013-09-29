using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models.Market
{
    public enum ResultCode
    {
        Successful = 0,
        MissingData,
        WrongEmail
    }
}