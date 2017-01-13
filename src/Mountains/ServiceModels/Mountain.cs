namespace Mountains.ServiceModels
{
    public sealed class Mountain
    {
        public Mountain(int id, string name, double latitude, double longitude, double elevation, double prominence, double isolation)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
            Prominence = prominence;
            Isolation = isolation;
        }

        public int Id { get; }

        public string Name { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public double Elevation { get; }

        public double Prominence { get; }

        public double Isolation { get; }
    }
}
