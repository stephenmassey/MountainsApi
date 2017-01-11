namespace Mountains.ServiceModels
{
    public sealed class Mountain
    {
        public Mountain(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}
