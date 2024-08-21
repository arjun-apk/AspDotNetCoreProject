using AspDotNetCoreProject.Context.Database;
using AspDotNetCoreProject.Context.User;
using AspDotNetCoreProject.Data.MySQLQuery;
using Dapper;
using System.Data;

namespace AspDotNetCoreProject.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseManager _databaseManager;

        public UserRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<long> CreateUser(UserEntity user)
        {
            using var _dbConnection = _databaseManager.GetConnection;
            return await _dbConnection.ExecuteScalarAsync<long>(UserQuery.CreateUser, user);
        }

        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            using var _dbConnection = _databaseManager.GetConnection;
            return await _dbConnection.QueryFirstOrDefaultAsync<UserEntity>(UserQuery.GetUserByUsername, new { Username = username });
        }
    }
}
