using Mountains.Common.Utility;

namespace Mountains.Users
{
    internal sealed class DbUser : DbObject<DbUser>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
