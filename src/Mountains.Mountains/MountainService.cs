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
(name, latitude, longitude, elevation, prominence, isolation, mountainRangeId)
VALUES (@name, @latitude, @longitude, @elevation, @prominence, @isolation, @mountainRangeId);
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

        public ReadOnlyCollection<Mountain> GetMountains(int start, int count, int? mountainRangeId = null)
        {
            string mountainRangeCondition = mountainRangeId.HasValue ? "WHERE mountainRangeId = @mountainRangeId" : "";
            string query = @"
SELECT " + DbMountain.GenerateColumns() + @"
FROM mountains
" + mountainRangeCondition + @"
LIMIT @start, @count";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbMountain>(query, new { start, count, mountainRangeId }).Select(MountainMapper.Map).ToList().AsReadOnly();
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
    isolation = @isolation,
    mountainRangeId = @mountainRangeId
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                connection.Execute(query, dbMountain);

            return MountainMapper.Map(dbMountain);
        }

        public MountainRange AddMountainRange(MountainRange mountainRange)
        {
            DbMountainRange dbMountainRange = MountainRangeMapper.Map(mountainRange);

            const string query = @"
INSERT INTO mountain_ranges
(name)
VALUES (@name);
SELECT LAST_INSERT_ID();";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                dbMountainRange.Id = connection.Query<int>(query, dbMountainRange).Single();

            return MountainRangeMapper.Map(dbMountainRange);
        }

        public void DeleteMountainRange(int id)
        {
            const string query = @"
DELETE FROM mountain_ranges
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                connection.Execute(query, new { id });
        }

        public MountainRange GetMountainRange(int id)
        {
            string query = @"
SELECT " + DbMountainRange.GenerateColumns() + @"
FROM mountain_ranges
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbMountainRange>(query, new { id }).Select(MountainRangeMapper.Map).SingleOrDefault();
        }

        public ReadOnlyCollection<MountainRange> GetMountainRanges(int start, int count)
        {
            string query = @"
SELECT " + DbMountainRange.GenerateColumns() + @"
FROM mountain_ranges
LIMIT @start, @count";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbMountainRange>(query, new { start, count }).Select(MountainRangeMapper.Map).ToList().AsReadOnly();
        }

        public MountainRange UpdateMountainRange(int id, MountainRange mountainRange)
        {
            DbMountainRange dbMountainRange = MountainRangeMapper.Map(mountainRange);
            dbMountainRange.Id = id;

            const string query = @"
UPDATE mountain_ranges SET
    name = @name
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                connection.Execute(query, dbMountainRange);

            return MountainRangeMapper.Map(dbMountainRange);
        }

        public Hike AddHike(Hike hike)
        {
            DbHike dbHike = HikeMapper.Map(hike);

            const string query = @"
INSERT INTO hikes
(mountainId, userId)
VALUES (@mountainId, @userId);
SELECT LAST_INSERT_ID();";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                dbHike.Id = connection.Query<int>(query, dbHike).Single();

            return HikeMapper.Map(dbHike);
        }

        public void DeleteHike(int id)
        {
            const string query = @"
DELETE FROM hikes
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                connection.Execute(query, new { id });
        }

        public Hike GetHike(int id)
        {
            string query = @"
SELECT " + DbHike.GenerateColumns() + @"
FROM hikes
WHERE id = @id";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbHike>(query, new { id }).Select(HikeMapper.Map).SingleOrDefault();
        }

        public ReadOnlyCollection<Hike> GetHikes(int start, int count)
        {
            string query = @"
SELECT " + DbHike.GenerateColumns() + @"
FROM hikes
LIMIT @start, @count";

            using (IDbConnection connection = DatabaseConnection.GetConnection())
                return connection.Query<DbHike>(query, new { start, count }).Select(HikeMapper.Map).ToList().AsReadOnly();
        }
    }
}
