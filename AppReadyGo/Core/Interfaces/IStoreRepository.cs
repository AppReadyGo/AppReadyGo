using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Interfaces
{
    public interface IStoreRepository
    {
        void AddPackageEvent(IPackageEvent packageEvent);
    }
}
