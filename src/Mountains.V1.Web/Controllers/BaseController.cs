using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public abstract class BaseController : ApiController
    {
        public int ParseId(string source)
        {
            int id;
            if (source == null || !int.TryParse(source, out id))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid id"));

            return id;
        }
    }
}
