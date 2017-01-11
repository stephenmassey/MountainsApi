using Mountains.Common.Properties;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Mountains.Common.MySql
{
    public static class DatabaseConnection
    {
        public static IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(GetConnectionString());
            conn.Open();
            return conn;
        }

        private static string GetConnectionString()
        {
            if (_connectionString != null)
                return _connectionString;

            _connectionString = ConfigurationManager.ConnectionStrings[Settings.Default.ConnectionString].ToString();

            return _connectionString;
        }

        private static string _connectionString;
    }
}
