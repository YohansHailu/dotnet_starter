using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text;


namespace Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //get the header
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("no Authorization Header"));
            }

            var AuthorizationHeader = Request.Headers.Authorization.ToString();

            if (!AuthorizationHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
            {

                return Task.FromResult(AuthenticateResult.Fail("Its not Basic Authorization"));
            }
            string usernamePassword = AuthorizationHeader.Replace("Basic", "", StringComparison.OrdinalIgnoreCase);

            string[] credentials = GetUserNamePassword(usernamePassword);

            if (credentials.Length != 2)
            {

                return Task.FromResult(AuthenticateResult.Fail("It doesn't contain passowrd or username"));
            }

            var client = new BasicAuthClient
            {
                IsAuthenticated = true,
                AuthenticationType = "Basic",
                Name = credentials[0]

            };

            string userName = credentials.First();
            string Password = credentials.Last();

            var identity = new ClaimsIdentity(client, new[] { new Claim("username", userName), new Claim("username", Password) });
            var principle = new ClaimsPrincipal(identity);

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principle, Scheme.Name)));
        }

        public static string[] GetUserNamePassword(string base64Credentials)
        {
            byte[] decodedBytes = Convert.FromBase64String(base64Credentials);
            string authString = Encoding.UTF8.GetString(decodedBytes);
            string[] credentials = authString.Split(':');

            credentials = credentials.Where(elmt => elmt.Length > 0).ToArray();


            return credentials;
        }
    }
}
