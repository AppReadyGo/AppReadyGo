using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.API.Models
{
    interface IMarketTestersRepository
    {
        IEnumerable<Tester> GetAll();
        Tester Get(string id);
        Tester Add(Tester item);
        void Remove(string id);
        bool Update(Tester item);
    }
}
