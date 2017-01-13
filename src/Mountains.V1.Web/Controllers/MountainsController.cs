using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;
using Mountains.V1.Web.DataMappers;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public sealed class MountainsController : BaseController
    {
        public MountainsController(IMountainService mountainService)
        {
            _mountainService = mountainService;
        }

        [HttpGet]
        [Route("mountains")]
        public MountainCollectionDto List(int start = 0, int? count = null)
        {
            return new MountainCollectionDto { Mountains = _mountainService.GetMountains(start, GetCount(count)).Select(MountainMapper.Map) };
        }

        [HttpGet]
        [Route("mountains/{id}")]
        public MountainDto Get(string id)
        {
            Mountain mountain = _mountainService.GetMountain(ParseId(id));

            if (mountain == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find mountain"));

            return MountainMapper.Map(mountain);
        }

        [HttpPost]
        [Route("mountains")]
        public MountainDto Post([FromBody]MountainDto mountainDto)
        {
            ValidateMountain(mountainDto);

            Mountain mountain = _mountainService.AddMountain(MountainMapper.Map(mountainDto));

            return MountainMapper.Map(mountain);
        }

        [HttpPut]
        [Route("mountains/{id}")]
        public MountainDto Put(string id, [FromBody]MountainDto mountainDto)
        {
            ValidateMountain(mountainDto);

            Mountain oldMountain = _mountainService.GetMountain(ParseId(id));

            if (oldMountain == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find mountain"));

            Mountain newMountain = _mountainService.UpdateMountain(ParseId(id), MountainMapper.Map(mountainDto));

            return MountainMapper.Map(newMountain);
        }

        [HttpDelete]
        [Route("mountains/{id}")]
        public void Delete(string id)
        {
            Mountain mountain = _mountainService.GetMountain(ParseId(id));

            if (mountain == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find mountain"));

            _mountainService.DeleteMountain(ParseId(id));
        }

        private void ValidateMountain(MountainDto mountain)
        {
            if (mountain == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply mountain"));

            if (mountain.Name == null || mountain.Name.Length < c_minNameLength || mountain.Name.Length > c_maxNameLength)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid name"));

            if (mountain.MountainRangeId != null && _mountainService.GetMountainRange(ParseId(mountain.MountainRangeId)) == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find moountain range"));
        }

        private IMountainService _mountainService;

        private const int c_minNameLength = 1;
        private const int c_maxNameLength = 250;
    }
}
