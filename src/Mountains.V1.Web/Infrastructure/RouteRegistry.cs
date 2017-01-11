using System.Web.Http;

namespace Mountains.V1.Web.Infrastructure
{
    public static class RouteRegistry
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Error",
                routeTemplate: "{*url}",
                defaults: new { controller = "Error" }
            );
        }
    }
}
