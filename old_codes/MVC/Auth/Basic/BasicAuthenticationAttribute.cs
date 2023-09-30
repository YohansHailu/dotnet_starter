using Microsoft.AspNetCore.Authorization;

namespace MVC.Auth.Basic
{

    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme;
        }

    }
}

