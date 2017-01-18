namespace Mountains.ServiceModels
{
    public sealed class User
    {
        public User(int id, string name, string email, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public int Id { get; }

        public string Name { get; }

        public string Email { get; }

        public string PasswordHash { get; }
    }
}
