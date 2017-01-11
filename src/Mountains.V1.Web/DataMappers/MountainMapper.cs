using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;

namespace Mountains.V1.Web.DataMappers
{
    internal static class MountainMapper
    {
        public static Mountain Map(MountainDto source)
        {
            if (source == null)
                return null;

            return new Mountain(0, source.Name);
        }

        public static MountainDto Map(Mountain source)
        {
            if (source == null)
                return null;

            return new MountainDto
            {
                Id = source.Id.ToString(),
                Name = source.Name,
            };
        }
    }
}
