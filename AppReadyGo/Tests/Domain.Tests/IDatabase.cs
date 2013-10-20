using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Domain.Tests
{
    internal interface IDatabase
    {
        void Clear();
        ISession OpenSession();
    }
}
