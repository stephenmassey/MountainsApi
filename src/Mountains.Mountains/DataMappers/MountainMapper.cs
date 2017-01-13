using Mountains.ServiceModels;

namespace Mountains.Mountains.DataMappers
{
    internal static class MountainMapper
    {
        public static Mountain Map(DbMountain source)
        {
            if (source == null)
                return null;

            return new Mountain(
                source.Id,
                source.Name,
                source.Latitude,
                source.Longitude,
                source.Elevation,
                source.Prominence,
                source.Isolation,
                source.MountainRangeId);
        }

        public static DbMountain Map(Mountain source)
        {
            if (source == null)
                return null;

            return new DbMountain
            {
                Id = source.Id,
                Name = source.Name,
                Latitude = source.Latitude,
                Longitude = source.Longitude,
                Elevation = source.Elevation,
                Isolation = source.Isolation,
                Prominence = source.Prominence,
                MountainRangeId = source.MountainRangeId,
            };
        }
    }
}
