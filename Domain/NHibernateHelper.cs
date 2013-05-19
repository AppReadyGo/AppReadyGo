using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Driver;
using NHibernate.Dialect;
using System.Configuration;
using NHibernate.Tool.hbm2ddl;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;

namespace AppReadyGo.Domain
{
    public sealed class NHibernateHelper
    {
        private ISessionFactory _sessionFactory;

        public static string NHibernateSQL { get; internal set; }

        public NHibernateHelper(string connectionString, SchemaAutoAction schemaAutoAction)
        {
            Init(connectionString, schemaAutoAction);
        }

        private void Init(string connectionString, SchemaAutoAction schemaAutoAction)
        {
            _sessionFactory = BuildSessionFactory(typeof(NHibernateHelper), connectionString, schemaAutoAction);
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        private static ISessionFactory BuildSessionFactory(Type type, string connectionString, SchemaAutoAction schemaAutoAction)
        {
            var mapper = new ModelMapper();

            mapper.AddMappings(type.Assembly.GetTypes());
            
            //mapper.AddMappings(typeof(OrganisationMapping).Assembly.GetTypes());

            var cfg = new NHibernate.Cfg.Configuration();

            cfg.DataBaseIntegration(c =>
            {
                c.ConnectionString = connectionString;
                c.Driver<SqlClientDriver>();
                c.Dialect<MsSql2008Dialect>();

                c.SchemaAction = schemaAutoAction;
#if DEBUG
                c.LogSqlInConsole = true;
                c.LogFormattedSql = true;
#endif
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;

            });

            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            SchemaMetadataUpdater.QuoteTableAndColumns(cfg);

            //var cmpl = mapper.CompileMappingFor(new List<Type>() { typeof(Application), typeof(Role), typeof(User) });
            //var cmpl = mapper.CompileMappingFor(new List<Type>() { typeof(Address), typeof(Organisation), typeof(OrganisationLicences), typeof(OrganisationJoined), typeof(Country), typeof(OrganisationType) });
            //var str = Serialize(cmpl);
            //Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception(str));

            return cfg.BuildSessionFactory();
        }


    }    

    public class NHSQLInterceptor : EmptyInterceptor, IInterceptor
    {
        NHibernate.SqlCommand.SqlString
            IInterceptor.OnPrepareStatement
                (NHibernate.SqlCommand.SqlString sql)
        {
            NHibernateHelper.NHibernateSQL = sql.ToString();
            return sql;
        }
    }
}
