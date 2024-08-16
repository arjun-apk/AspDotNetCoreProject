using AspDotNetCoreProject.Context.Common;
using AspDotNetCoreProject.Context.Database;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace AspDotNetCoreProject.Data
{
    public class DatabaseManager : IDatabaseManager
    {
        private IDbConnection? _connection;
        private string _connectionString { get; set; }

        public DatabaseManager(IOptions<AppSettings> appSettings)
        {
            _connectionString = appSettings.Value.DatabaseConfig.ConnectionString;
        }

        public IDbConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new MySqlConnection(_connectionString);
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection?.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
