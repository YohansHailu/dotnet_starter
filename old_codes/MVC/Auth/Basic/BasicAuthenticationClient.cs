using System.Security.Principal;

namespace MVC.Auth.Basic
{
    public class BasicAuthenticationClient : IIdentity
    {
        public string? AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Name { get; set; }
    }
}


// authentication handele
