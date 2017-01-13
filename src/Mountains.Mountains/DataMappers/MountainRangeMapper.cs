using Mountains.ServiceModels;

namespace Mountains.Mountains.DataMappers
{
    internal static class MountainRangeMapper
    {
        public static MountainRange Map(DbMountainRange source)
        {
            if (source == null)
                return null;

            return new MountainRange(
                source.Id,
                source.Name);
        }

        public static DbMountainRange Map(MountainRange source)
        {
            if (source == null)
                return null;

            return new DbMountainRange
            {
                Id = source.Id,
                Name = source.Name,
            };
        }
    }
}
