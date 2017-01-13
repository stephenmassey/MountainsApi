using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;
using Mountains.V1.Web.DataMappers;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public sealed class MountainRangesController : BaseController
    {
        public MountainRangesController(IMountainService mountainService)
        {
            _mountainService = mountainService;
        }

        [HttpGet]
        [Route("mountainranges")]
        public MountainRangeCollectionDto Get()
        {
            return new MountainRangeCollectionDto { MountainRanges = _mountainService.GetMountainRanges().Select(MountainRangeMapper.Map) };
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

        [HttpPost]
        [Route("mountainranges")]
        public MountainRangeDto Post([FromBody]MountainRangeDto mountainRangeDto)
        {
            if (mountainRangeDto == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply mountain range"));

            MountainRange mountainRange = _mountainService.AddMountainRange(MountainRangeMapper.Map(mountainRangeDto));

            return MountainRangeMapper.Map(mountainRange);
        }

        [HttpPut]
        [Route("mountainranges/{id}")]
        public MountainRangeDto Put(string id, [FromBody]MountainRangeDto mountainRangeDto)
        {
            if (mountainRangeDto == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply mountain range"));

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

        private IMountainService _mountainService;
    }
}
