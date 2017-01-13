using Mountains.Common.Utility;

namespace Mountains.Mountains
{
    internal sealed class DbMountainRange : DbObject<DbMountainRange>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
