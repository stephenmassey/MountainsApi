using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;

namespace Mountains.V1.Web.DataMappers
{
    internal static class UserMapper
    {
        public static User Map(UserDto source, string email, string passwordHash)
        {
            if (source == null)
                return null;

            return new User(
                0,
                source.Name,
                email,
                passwordHash);
        }

        public static UserDto Map(User source)
        {
            if (source == null)
                return null;

            return new UserDto
            {
                Id = source.Id.ToString(),
                Name = source.Name,
            };
        }
    }
}
