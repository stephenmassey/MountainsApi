using Dapper;
using Mountains.Common.MySql;
using Mountains.Mountains.DataMappers;
using Mountains.ServiceModels;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Mountains.Mountains
{
    public sealed class MountainService : IMountainService
    {
        public MountainService()
        {
        }

        public Mountain AddMountain(Mountain mountain)
        {
            DbMountain dbMountain = MountainMapper.Map(mountain);

            const string query = @"
INSERT INTO mountains
(name, latitude, longitude, elevation, prominence, isolation)
VALUES (@name, @latitude, @longitude, @elevation, @prominence, @isolation);
SELECT LAST_INSERT_ID();";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                dbMountain.Id = connection.Query<int>(query, dbMountain).Single();

            return MountainMapper.Map(dbMountain);
        }

        public void DeleteMountain(int id)
        {
            const string query = @"
DELETE FROM mountains
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                connection.Execute(query, new { id });
        }

        public Mountain GetMountain(int id)
        {
            string query = @"
SELECT " + DbMountain.GenerateColumns() + @"
FROM mountains
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbMountain>(query, new { id }).Select(MountainMapper.Map).SingleOrDefault();
        }

        public ReadOnlyCollection<Mountain> GetMountains()
        {
            string query = @"
SELECT " + DbMountain.GenerateColumns() + @"
FROM mountains";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbMountain>(query).Select(MountainMapper.Map).ToList().AsReadOnly();
        }

        public Mountain UpdateMountain(int id, Mountain mountain)
        {
            DbMountain dbMountain = MountainMapper.Map(mountain);
            dbMountain.Id = id;

            const string query = @"
UPDATE mountains SET
    name = @name,
    latitude = @latitude,
    longitude = @longitude,
    elevation = @elevation,
    prominence = @prominence,
    isolation = @isolation
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                connection.Execute(query, dbMountain);

            return MountainMapper.Map(dbMountain);
        }
    }
}
