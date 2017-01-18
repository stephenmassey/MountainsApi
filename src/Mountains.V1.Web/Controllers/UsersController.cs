using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;
using Mountains.V1.Web.DataMappers;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public sealed class UsersController : BaseController
    {
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        public UserCollectionDto List(int start = 0, int? count = null)
        {
            return new UserCollectionDto { Users = _userService.GetUsers(start, GetCount(count)).Select(UserMapper.Map) };
        }

        [HttpGet]
        [Route("users/{id}")]
        public UserDto Get(string id)
        {
            User user = _userService.GetUser(ParseId(id));

            if (user == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find user"));

            return UserMapper.Map(user);
        }

        [HttpPost]
        [Route("users")]
        public UserDto Create([FromBody]UserDto userDto)
        {
            ValidateUser(userDto);

            User user = _userService.AddUser(UserMapper.Map(userDto));

            return UserMapper.Map(user);
        }

        [HttpPut]
        [Route("users/{id}")]
        public UserDto Update(string id, [FromBody]UserDto userDto)
        {
            ValidateUser(userDto);

            User oldUser = _userService.GetUser(ParseId(id));

            if (oldUser == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find user"));

            User newUser = _userService.UpdateUser(ParseId(id), UserMapper.Map(userDto));

            return UserMapper.Map(newUser);
        }

        private void ValidateUser(UserDto user)
        {
            if (user == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply user"));

            if (user.Name == null || user.Name.Length < c_minNameLength || user.Name.Length > c_maxNameLength)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid name"));
        }

        private IUserService _userService;

        private const int c_minNameLength = 1;
        private const int c_maxNameLength = 250;
    }
}
