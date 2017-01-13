using Mountains.Common.Utility;

namespace Mountains.Mountains
{
    internal sealed class DbMountain : DbObject<DbMountain>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public double Prominence { get; set; }
        public double Isolation { get; set; }
        public int? MountainRangeId { get; set; }
    }
}
