namespace Mountains.V1.Client.Dtos
{
    public sealed class MountainDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public double Prominence { get; set; }
        public double Isolation { get; set; }
        public string MountainRangeId { get; set; }
    }
}
