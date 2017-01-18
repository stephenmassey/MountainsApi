namespace Mountains.ServiceModels
{
    public sealed class Hike
    {
        public Hike(int id, int mountainId, int userId)
        {
            Id = id;
            MountainId = mountainId;
            UserId = userId;
        }

        public int Id { get; }

        public int MountainId { get; }

        public int UserId { get; }
    }
}
