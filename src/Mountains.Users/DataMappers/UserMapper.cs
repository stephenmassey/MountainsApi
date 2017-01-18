using Mountains.ServiceModels;

namespace Mountains.Users.DataMappers
{
    internal static class UserMapper
    {
        public static User Map(DbUser source)
        {
            if (source == null)
                return null;

            return new User(
                source.Id,
                source.Name,
                source.Email,
                source.PasswordHash);
        }

        public static DbUser Map(User source)
        {
            if (source == null)
                return null;

            return new DbUser
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                PasswordHash = source.PasswordHash,
            };
        }
    }
}
