using Mountains.Common.Utility;
using Mountains.ServiceModels;
using Mountains.V1.Client.Dtos;
using Mountains.V1.Web.DataMappers;
using Mountains.V1.Web.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Mountains.V1.Web.Controllers
{
    public sealed class UsersController : BaseController
    {
        public UsersController(AuthenticationService authenticationService, IUserService userService)
            : base(authenticationService)
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

        [HttpGet]
        [Route("users/currentUser")]
        public UserDto GetCurrentUser()
        {
            if (!AuthenticationService.IsAuthenticated)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ""));

            User user = _userService.GetUser(AuthenticationService.UserId);

            if (user == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find user"));

            return UserMapper.Map(user);
        }

        [HttpPost]
        [Route("users")]
        public UserDto Create([FromBody]UserDto userDto)
        {
            ValidateUserForCreate(userDto);

            if (_userService.GetUserByEmail(userDto.Email) != null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ""));

            User user = _userService.AddUser(UserMapper.Map(userDto, userDto.Email, PasswordHasher.HashPassword(userDto.Password)));

            return UserMapper.Map(user);
        }

        [HttpPut]
        [Route("users/{id}")]
        public UserDto Update(string id, [FromBody]UserDto userDto)
        {
            int userId = ParseId(id);

            if (userId != AuthenticationService.UserId || _userService.GetUser(userId) == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find user"));

            ValidateUserForUpdate(userDto);

            User newUser = _userService.UpdateUser(userId, UserMapper.Map(userDto, null, null));

            return UserMapper.Map(newUser);
        }

        [HttpPost]
        [Route("users/signin")]
        public HttpResponseMessage SignIn([FromBody]UserDto userDto)
        {
            if (userDto.Email == null || userDto.Password == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply user name and password"));

            User user = _userService.GetUserByEmail(userDto.Email);

            if (user == null || !PasswordHasher.ValidatePassword(userDto.Password, user.PasswordHash))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invlaide username or password"));

            HttpResponseMessage response = new HttpResponseMessage();

            response.Content = new ObjectContent<UserDto>(UserMapper.Map(user), new JsonMediaTypeFormatter());
            response.Headers.AddCookies(new CookieHeaderValue[] { AuthenticationService.CreateAuthenticationCookie(user) });
            return response;
        }

        [HttpDelete]
        [Route("users/signout")]
        public HttpResponseMessage SignOut()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            response.Headers.AddCookies(new CookieHeaderValue[] { AuthenticationService.CreateAnonymousAuthenticationCookie() });
            return response;
        }

        private void ValidateUserForCreate(UserDto user)
        {
            ValidateUserForUpdate(user);

            if (user.Email == null || user.Email.Length < c_minNameLength || user.Email.Length > c_maxNameLength || !Regex.IsMatch(user.Email, c_emailRegex, RegexOptions.IgnoreCase))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid email"));

            if (user.Email == null || user.Email.Length < c_minPasswordLength || user.Email.Length > c_maxNameLength)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid password"));
        }

        private void ValidateUserForUpdate(UserDto user)
        {
            if (user == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply user"));

            if (user.Name == null || user.Name.Length < c_minNameLength || user.Name.Length > c_maxNameLength)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid name"));
        }

        private readonly IUserService _userService;

        private const int c_minPasswordLength = 8;
        private const int c_minNameLength = 1;
        private const int c_maxNameLength = 250;
        private const string c_emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
    }
}
