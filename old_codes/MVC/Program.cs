using Microsoft.AspNetCore.Authentication;
using MVC.Models.option;
using MVC.Auth.Basic;
using MVC.Auth.Jwt;
using MVC.Auth;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//

builder.Services.AddSingleton<Greet>();
builder.Services.AddOptions<Woption>().BindConfiguration(nameof(Woption)).ValidateDataAnnotations();
builder.Services.AddOptions<JwtBearerSettings>().Bind(builder.Configuration.GetSection("JwtBearer")).ValidateDataAnnotations().ValidateOnStart();



// authentication
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null).AddScheme<JwtBearerOptions, JwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        var jwtBearerSettings = builder.Configuration.GetSection("JwtBearer").Get<JwtBearerSettings>();
        if (jwtBearerSettings == null)
        {
            throw new NullReferenceException("The 'JwtBearer' section cannot be found in the configuration");
        }

        if (jwtBearerSettings.SigningKey == null)
        {
            throw new NullReferenceException("The 'SigningKey' section cannot be found in the configuration");
        }

        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = jwtBearerSettings.Issuer,
            ValidAudience = jwtBearerSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearerSettings.SigningKey)),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };

    });



builder.Services.AddControllersWithViews();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("print", () =>
{
    return JwtBearerDefaults.AuthenticationScheme;
});


app.MapControllerRoute(name: "default",
               pattern: "auth/{controller}/{action}/");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/").RequireAuthorization();




app.Run();
