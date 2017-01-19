using Mountains.V1.Web.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public sealed class ErrorController : BaseController
    {
        public ErrorController(AuthenticationService authenticationService)
            : base(authenticationService)
        {
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, HttpPatch]
        public object Error()
        {
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "this item does not exist"));
        }
    }
}
