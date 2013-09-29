using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
    public static class Global
    {
#if QA
        public static readonly Uri BaseAddress = new Uri("http://qa.appreadygo.com/");
        public static readonly Uri ApiBaseAddress = new Uri("http://api.qa.appreadygo.com/");
#elif DEBUG
        public static readonly Uri BaseAddress = new Uri("http://localhost:63224/");
        public static readonly Uri ApiBaseAddress = new Uri("");
#else
        public static readonly Uri BaseAddress = new Uri("http://appreadygo.com/");
        public static readonly Uri ApiBaseAddress = new Uri("http://api.appreadygo.com/");
#endif
    }
}
