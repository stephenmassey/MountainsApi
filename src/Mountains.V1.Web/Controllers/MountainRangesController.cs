using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;
using Mountains.V1.Web.DataMappers;
using Mountains.V1.Web.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public sealed class MountainRangesController : BaseController
    {
        public MountainRangesController(AuthenticationService authenticationService, IMountainService mountainService)
            : base(authenticationService)
        {
            _mountainService = mountainService;
        }

        [HttpGet]
        [Route("mountainranges")]
        public MountainRangeCollectionDto List(int start = 0, int? count = null)
        {
            return new MountainRangeCollectionDto { MountainRanges = _mountainService.GetMountainRanges(start, GetCount(count)).Select(MountainRangeMapper.Map) };
        }

        [HttpGet]
        [Route("mountainranges/{id}")]
        public MountainRangeDto Get(string id)
        {
            MountainRange mountainRange = _mountainService.GetMountainRange(ParseId(id));

            if (mountainRange == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find mountain range"));

            return MountainRangeMapper.Map(mountainRange);
        }

        [HttpGet]
        [Route("mountainranges/{id}/mountains")]
        public MountainCollectionDto ListMountainsinRange(string id, int start = 0, int? count = null)
        {
            MountainRange mountainRange = _mountainService.GetMountainRange(ParseId(id));

            if (mountainRange == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find mountain range"));

            return new MountainCollectionDto { Mountains = _mountainService.GetMountains(start, GetCount(count), mountainRange.Id).Select(MountainMapper.Map) };
        }

        [HttpPost]
        [Route("mountainranges")]
        public MountainRangeDto Post([FromBody]MountainRangeDto mountainRangeDto)
        {
            ValidateMountainRange(mountainRangeDto);

            MountainRange mountainRange = _mountainService.AddMountainRange(MountainRangeMapper.Map(mountainRangeDto));

            return MountainRangeMapper.Map(mountainRange);
        }

        [HttpPut]
        [Route("mountainranges/{id}")]
        public MountainRangeDto Put(string id, [FromBody]MountainRangeDto mountainRangeDto)
        {
            ValidateMountainRange(mountainRangeDto);

            MountainRange oldMountainRange = _mountainService.GetMountainRange(ParseId(id));

            if (oldMountainRange == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find mountain range"));

            MountainRange newMountainRange = _mountainService.UpdateMountainRange(ParseId(id), MountainRangeMapper.Map(mountainRangeDto));

            return MountainRangeMapper.Map(newMountainRange);
        }

        [HttpDelete]
        [Route("mountainranges/{id}")]
        public void Delete(string id)
        {
            MountainRange mountainRange = _mountainService.GetMountainRange(ParseId(id));

            if (mountainRange == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find mountain range"));

            _mountainService.DeleteMountainRange(ParseId(id));
        }

        private void ValidateMountainRange(MountainRangeDto mountainRange)
        {
            if (mountainRange == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply mountain range"));

            if (mountainRange.Name == null || mountainRange.Name.Length < c_minNameLength || mountainRange.Name.Length > c_maxNameLength)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid name"));
        }

        private readonly IMountainService _mountainService;

        private const int c_minNameLength = 1;
        private const int c_maxNameLength = 250;
    }
}
