using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.Core.Logger;
using NHibernate;
using NHibernate.SqlCommand;

namespace AppReadyGo.Domain
{
    public class SQLInterceptor : EmptyInterceptor, IInterceptor
    {
        private static readonly ApplicationLogging log = new ApplicationLogging(MethodBase.GetCurrentMethod().DeclaringType);

        SqlString IInterceptor.OnPrepareStatement(SqlString sql)
        {
            log.WriteVerbose(sql.ToString());
            return sql;
        }
    }
}
