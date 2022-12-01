using Microsoft.AspNetCore.Authentication.Cookies;

namespace Mango.Web
{
    public static class Utils
    {
        public static string APIBase { get; set; }

        public const string CookieName = CookieAuthenticationDefaults.AuthenticationScheme;

        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string Customer = "Customer";
        }

    public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE    
        }
    }
}
