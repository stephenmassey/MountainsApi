using Dapper;
using Mountains.Common.MySql;
using Mountains.ServiceModels;
using Mountains.Users.DataMappers;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Mountains.Users
{
    public sealed class UserService : IUserService
    {
        public UserService()
        {
        }

        public User AddUser(User user)
        {
            DbUser dbUser = UserMapper.Map(user);

            const string query = @"
INSERT INTO users
(name)
VALUES (@name);
SELECT LAST_INSERT_ID();";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                dbUser.Id = connection.Query<int>(query, dbUser).Single();

            return UserMapper.Map(dbUser);
        }

        public User GetUser(int id)
        {
            string query = @"
SELECT " + DbUser.GenerateColumns() + @"
FROM users
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbUser>(query, new { id }).Select(UserMapper.Map).SingleOrDefault();
        }

        public ReadOnlyCollection<User> GetUsers(int start, int count)
        {
            string query = @"
SELECT " + DbUser.GenerateColumns() + @"
FROM users
LIMIT @start, @count";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbUser>(query, new { start, count }).Select(UserMapper.Map).ToList().AsReadOnly();
        }

        public User UpdateUser(int id, User user)
        {
            DbUser dbUser = UserMapper.Map(user);
            dbUser.Id = id;

            const string query = @"
UPDATE users SET
    name = @name
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                connection.Execute(query, dbUser);

            return UserMapper.Map(dbUser);
        }
    }
}
