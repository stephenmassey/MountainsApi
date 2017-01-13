namespace Mountains.ServiceModels
{
    public sealed class MountainRange
    {
        public MountainRange(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
