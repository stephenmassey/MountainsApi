using Mountains.ServiceModels;

namespace Mountains.Mountains.DataMappers
{
    internal static class MountainMapper
    {
        public static Mountain Map(DbMountain source)
        {
            if (source == null)
                return null;

            return new Mountain(source.Id, source.Name);
        }

        public static DbMountain Map(Mountain source)
        {
            if (source == null)
                return null;

            return new DbMountain
            {
                Id = source.Id,
                Name = source.Name,
            };
        }
    }
}
