using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace SupplierWebApi.Dapper.Connections
{
    public class SupDbConnetion : IDisposable
    {
        private readonly string _dbConnection;
        //public IConfiguration config { get; }
        private readonly IList<MySqlConnection> connections = new List<MySqlConnection>();

        public SupDbConnetion()
        {
            //_dbConnection = new ConfigurationHelper().GetAppSettings<AppSetting>("ConnectionStrings").SupBack;
            _dbConnection = new ConfigurationHelper().GetConnectionString();
        }

        public void Dispose()
        {
            foreach (var item in connections)
            {
                item.Close();
                item.Dispose();
            }
        }

        public MySqlConnection GetDbConnection()
        {
            var conn = new MySqlConnection(_dbConnection);
            connections.Add(conn);
            return conn;
        }
    }
}
