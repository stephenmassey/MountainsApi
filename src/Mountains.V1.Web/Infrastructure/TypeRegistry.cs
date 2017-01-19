using Mountains.Mountains;
using Mountains.Users;
using StructureMap;
using System.Web;
using System.Web.Security;

namespace Mountains.V1.Web.Infrastructure
{
    public static class TypeRegistry
    {
        public static IContainer RegisterTypes()
        {
            return new Container(container =>
            {
                container.For<AuthenticationService>().HttpContextScoped().Use(c => AuthenticationService.Initialize(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName]));
                container.For<IUserService>().Singleton().Use<UserService>();
                container.For<IMountainService>().Singleton().Use<MountainService>();
            });
        }
    }
}
