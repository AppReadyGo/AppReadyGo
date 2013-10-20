using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Domain;
using NHibernate;
using NHibernate.Cfg;

namespace Domain.Tests
{
    internal class RemoteDatabase : IDatabase
    {
        public string ConnectionString { get; private set; }
        private NHibernateHelper nhibernateHelper;
       
        public RemoteDatabase(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.nhibernateHelper = new NHibernateHelper(this.ConnectionString, SchemaAutoAction.Validate);
        }

        #region IDatabase Members

        public void Clear()
        {
            
        }

        public ISession OpenSession()
        {
            return nhibernateHelper.OpenSession();
        }

        #endregion
    }
}
