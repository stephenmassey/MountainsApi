using Mountains.Mountains;
using StructureMap;

namespace Mountains.V1.Web.Infrastructure
{
    public static class TypeRegistry
    {
        public static IContainer RegisterTypes()
        {
            return new Container(container =>
            {
                container.For<IMountainService>().Singleton().Use<MountainService>();
            });
        }
    }
}
