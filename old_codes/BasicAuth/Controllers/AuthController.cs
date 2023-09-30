using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Auth;




namespace Controllers;

public class AuthController : Controller
{
    public JwtSettings _jwtSettings;
    public AuthController(IOptions<JwtSettings> JwtSettings)
    {
        _jwtSettings = JwtSettings.Value;
    }


    [BasicAutherization]
    public IActionResult Login()
    {

        // creat the token

        // you need identity
        // token handler

        var my_identity = new JwtClient()
        {
            AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
            IsAuthenticated = true,
            Name = "demekqe"
        };

        var identy = new ClaimsIdentity(my_identity, new List<Claim> { new Claim("name", "demekqew") });


        var now = DateTime.UtcNow;
        //var expiry = now.Add(TimeSpan.FromSeconds(1));
        //var expiry = now.Add(TimeSpan.FromSeconds(10));
        var expiry = now.Add(TimeSpan.FromHours(1));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDiscription = new SecurityTokenDescriptor()
        {
            Subject = identy,
            Expires = expiry,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)), SecurityAlgorithms.HmacSha512Signature),
            IssuedAt = now,
            NotBefore = now,
        };


        // token discription
        // token creator
        // token writer

        return Ok(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDiscription)));
    }
}
