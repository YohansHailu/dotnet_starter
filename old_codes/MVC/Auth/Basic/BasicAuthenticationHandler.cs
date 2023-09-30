using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text;
using System.Security.Claims;

namespace MVC.Auth.Basic
{

    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("No autheraztion on the header"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization header does not start with 'Basic'"));
            }


            var authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)));
            var authSplit = authBase64Decoded.Split(':', 2);

            if (authSplit.Length != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header format"));
            }

            var clientId = authSplit.First();
            var clientSecret = authSplit.Last();


            var client = new BasicAuthenticationClient
            {
                AuthenticationType = "Basic",
                IsAuthenticated = true,
                Name = clientId
            };

            // var claimsPrincipal = new ClaimsPrincipal();
            // var myIdentity = new ClaimsIdentity();
            // var claim = new Claim(ClaimTypes.Name, clientId);

            // myIdentity.AddClaim(claim);
            // claimsPrincipal.AddIdentity(myIdentity);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, clientId)
            };

            var identity = new ClaimsIdentity(client, claims);

            var claimsPrincipal = new ClaimsPrincipal(identity);


            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));


        }


    }
}

