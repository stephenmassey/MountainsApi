using Mountains.Common.Utility;

namespace Mountains.Mountains
{
    internal sealed class DbHike : DbObject<DbHike>
    {
        public int Id { get; set; }
        public int MountainId { get; set; }
        public int UserId { get; set; }
    }
}
