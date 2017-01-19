using Mountains.ServiceModels;
using System;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Security;

namespace Mountains.V1.Web.Infrastructure
{
    public sealed class AuthenticationService
    {
        public static AuthenticationService Initialize(HttpCookie authCookie)
        {
            if (authCookie != null && authCookie.Value != null && authCookie.Value != "")
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                int userId;
                if (int.TryParse(new FormsIdentity(authTicket).Name, out userId))
                    return new AuthenticationService(userId);
            }

            return new AuthenticationService(c_anonymousUserId);
        }

        public int UserId { get { return _userId; } }

        public bool IsAuthenticated { get { return _userId != c_anonymousUserId; } }

        public CookieHeaderValue CreateAuthenticationCookie(User user)
        {
            return CreateAuthenticationCookie(user.Id, DateTime.UtcNow.AddYears(1));
        }

        public CookieHeaderValue CreateAnonymousAuthenticationCookie()
        {
            return CreateAuthenticationCookie(c_anonymousUserId, DateTime.UtcNow.AddHours(-1));
        }

        private CookieHeaderValue CreateAuthenticationCookie(int userId, DateTime expirationDate)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
              c_ticketVersion,
              userId.ToString(),
              DateTime.UtcNow,
              expirationDate,
              false,
              "",
              FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new CookieHeaderValue(FormsAuthentication.FormsCookieName, encryptedTicket);

            cookie.Domain = ".awesome.com";
            cookie.Expires = expirationDate;
            cookie.Path = "/";

            return cookie;
        }

        private AuthenticationService(int userId)
        {
            _userId = userId;
        }

        private int _userId;

        private const int c_ticketVersion = 1;
        private const int c_anonymousUserId = -1;
    }
}
