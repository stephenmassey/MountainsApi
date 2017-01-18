using Mountains.V1.Web.Infrastructure;
using Newtonsoft.Json;
using StructureMap;
using System.Web;
using System.Web.Http;
using WebApiContrib.IoC.StructureMap;

namespace Mountains.V1.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            _container = TypeRegistry.RegisterTypes();
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            GlobalConfiguration.Configure(RouteRegistry.RegisterRoutes);

            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapResolver(_container);
        }

        protected void Application_End()
        {
            //Dispose of containers to make sure Singleton Scoped dependencies get disposed
            _container.Dispose();
        }

        private IContainer _container;
    }
}
