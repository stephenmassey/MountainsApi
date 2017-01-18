using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected int ParseId(string source)
        {
            int id;
            if (source == null || !int.TryParse(source, out id))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid id"));

            return id;
        }

        protected int? ParseIdOrDefault(string source)
        {
            if (source == null)
                return null;

            return ParseId(source);
        }

        protected int GetCount(int? count)
        {
            if (!count.HasValue)
                return c_count;

            return Math.Min(count.Value, c_maxCount);
        }

        private const int c_count = 10;
        private const int c_maxCount = 100;
    }
}
