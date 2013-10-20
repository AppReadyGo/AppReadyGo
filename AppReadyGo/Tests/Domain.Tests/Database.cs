using AppReadyGo.Domain;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests
{
    internal class Database : IDatabase
    {
        public string ServerName { get; private set; }
        public string DatabaseName { get; private set; }
        public string ConnectionString { get; private set; }

        private NHibernateHelper nhibernateHelper;

        public Database(string serverName, string databaseName)
        {
            this.ServerName = serverName;
            this.DatabaseName = databaseName;
            this.ConnectionString = "SERVER = " + serverName + "; DATABASE = " + databaseName + "; User ID = sa; Pwd = sa";

            this.Clear();
            CreateDatabase(this.ServerName, this.DatabaseName);

            this.nhibernateHelper = new NHibernateHelper(this.ConnectionString, SchemaAutoAction.Create);
        }

        private void CreateDatabase(string serverName, string databaseName)
        {
            string filesPath = @"C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\";//Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            var conn = new SqlConnection();
            conn.ConnectionString = "SERVER = " + serverName + "; DATABASE = master; User ID = sa; Pwd = sa";
            string sqlCreateDBQuery = " CREATE DATABASE "
                               + databaseName
                               + " ON PRIMARY "
                               + " (NAME = N'" + databaseName + "', "
                               + " FILENAME = N'" + filesPath + databaseName + ".mdf', "
                               + " SIZE = 2MB,"
                               + " FILEGROWTH = 1024KB) "
                               + " LOG ON (NAME = N'" + databaseName + "_log', "
                               + " FILENAME = N'" + filesPath + databaseName + "_log.ldf', "
                               + " SIZE = 1MB, "
                               + " FILEGROWTH = 10%);";

            RunCommand(conn, sqlCreateDBQuery);

            conn = new SqlConnection();
            conn.ConnectionString = this.ConnectionString;

            RunCommand(conn, "CREATE SCHEMA [api] AUTHORIZATION [dbo];");
            RunCommand(conn, "CREATE SCHEMA [cont] AUTHORIZATION [dbo];");
            RunCommand(conn, "CREATE SCHEMA [usr] AUTHORIZATION [dbo];");
            RunCommand(conn, "CREATE SCHEMA [utls] AUTHORIZATION [dbo];");
            RunCommand(conn, "CREATE SCHEMA [log] AUTHORIZATION [dbo];");
        }

        private void RunCommand(SqlConnection conn, string command)
        {
            SqlCommand myCommand = new SqlCommand(command, conn);
            try
            {
                conn.Open();
                myCommand.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public void Clear()
        {
            var conn = new SqlConnection();

            conn.ConnectionString = "SERVER = " + this.ServerName + "; DATABASE = master; User ID = sa; Pwd = sa";
            string sqlCreateDBQuery = " IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '" + this.DatabaseName + "' OR name = '" + this.DatabaseName + "')))"
                                    + "BEGIN"
                                    + " ALTER DATABASE " + this.DatabaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;"
                                    + "	DROP DATABASE " + this.DatabaseName + ";"
                                    + "END";
            RunCommand(conn, sqlCreateDBQuery);
        }

        public ISession OpenSession()
        {
            return nhibernateHelper.OpenSession();
        }
    }
}
