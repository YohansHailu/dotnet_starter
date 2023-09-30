using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using MVC.Auth.Jwt;
using MVC.Auth.Basic;
using System.Text;

namespace MVC.Controllers;


public class AuthController : Controller
{

    public readonly JwtBearerSettings _jwtBearerSettings;
    public AuthController(IOptions<JwtBearerSettings> jwtSettings)
    {
        _jwtBearerSettings = jwtSettings.Value;

    }

    [BasicAuthorization]
    public IActionResult Login()
    {

        var now = DateTime.UtcNow;
        var expiry = now.Add(TimeSpan.FromHours(1));


        // generate the token
        //

        var tokenHandler = new JwtSecurityTokenHandler();

        var my_identity = new JwtBearerClient()
        {
            AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
            IsAuthenticated = true,
            Name = "demekqe"
        };

        var identty = new ClaimsIdentity(my_identity, new List<Claim>
                {
                    { new Claim(JwtRegisteredClaimNames.Name, "Round The Code") }
                });

        Console.Write(_jwtBearerSettings.SigningKey);
        var tokenDiscription = new SecurityTokenDescriptor()
        {
            Subject = identty,
            Expires = expiry,
            Issuer = _jwtBearerSettings.Issuer,
            Audience = _jwtBearerSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSettings.SigningKey)), SecurityAlgorithms.HmacSha512Signature),
            IssuedAt = now,
            NotBefore = now,
        };

        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDiscription));

        return Ok(new { access_token = token, token_type = JwtBearerDefaults.AuthenticationScheme, expires_in = expiry.Subtract(DateTime.UtcNow).TotalSeconds.ToString("0") });
    }
}
