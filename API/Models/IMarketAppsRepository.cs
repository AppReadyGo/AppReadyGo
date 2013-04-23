using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.API.Models
{
    interface IMarketAppsRepository
    {
        IEnumerable<Application> GetAll();
        Application Get(string id);
        Application Add(Application item);
        void Remove(string id);
        bool Update(Application item);
    }

    
}
