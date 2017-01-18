using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;

namespace Mountains.V1.Web.DataMappers
{
    internal static class HikeMapper
    {
        public static Hike Map(HikeDto source)
        {
            if (source == null)
                return null;

            return new Hike(
                0,
                int.Parse(source.MountainId),
                int.Parse(source.UserId));
        }

        public static HikeDto Map(Hike source)
        {
            if (source == null)
                return null;

            return new HikeDto
            {
                Id = source.Id.ToString(),
                MountainId = source.MountainId.ToString(),
                UserId = source.UserId.ToString(),
            };
        }
    }
}
