using Mountains.ServiceModels;
using System.Collections.ObjectModel;

namespace Mountains
{
    public interface IUserService
    {
        User GetUser(int id);

        ReadOnlyCollection<User> GetUsers(int start, int count);

        User AddUser(User user);

        User UpdateUser(int id, User user);
    }
}
