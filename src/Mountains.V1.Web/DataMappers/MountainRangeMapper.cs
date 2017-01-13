using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;

namespace Mountains.V1.Web.DataMappers
{
    internal static class MountainRangeMapper
    {
        public static MountainRange Map(MountainRangeDto source)
        {
            if (source == null)
                return null;

            return new MountainRange(
                0,
                source.Name);
        }

        public static MountainRangeDto Map(MountainRange source)
        {
            if (source == null)
                return null;

            return new MountainRangeDto
            {
                Id = source.Id.ToString(),
                Name = source.Name,
            };
        }
    }
}
