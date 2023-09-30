using Microsoft.AspNetCore.Authorization;

namespace Auth
{
    public class BasicAutherizationAttribute : AuthorizeAttribute
    {
        public BasicAutherizationAttribute()
        {
            AuthenticationSchemes = "Basic";
        }

    }
}

