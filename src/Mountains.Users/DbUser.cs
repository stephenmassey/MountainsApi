using Mountains.Common.Utility;

namespace Mountains.Users
{
    internal sealed class DbUser : DbObject<DbUser>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
