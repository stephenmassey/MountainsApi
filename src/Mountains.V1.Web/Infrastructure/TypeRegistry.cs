using Mountains.Mountains;
using Mountains.Users;
using StructureMap;

namespace Mountains.V1.Web.Infrastructure
{
    public static class TypeRegistry
    {
        public static IContainer RegisterTypes()
        {
            return new Container(container =>
            {
                container.For<IUserService>().Singleton().Use<UserService>();
                container.For<IMountainService>().Singleton().Use<MountainService>();
            });
        }
    }
}
