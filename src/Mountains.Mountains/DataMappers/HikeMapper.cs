using Mountains.ServiceModels;

namespace Mountains.Mountains.DataMappers
{
    internal static class HikeMapper
    {
        public static Hike Map(DbHike source)
        {
            if (source == null)
                return null;

            return new Hike(
                source.Id,
                source.MountainId,
                source.UserId);
        }

        public static DbHike Map(Hike source)
        {
            if (source == null)
                return null;

            return new DbHike
            {
                Id = source.Id,
                MountainId = source.MountainId,
                UserId = source.UserId,
            };
        }
    }
}
