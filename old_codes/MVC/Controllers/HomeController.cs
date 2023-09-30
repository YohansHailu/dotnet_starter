using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Microsoft.Extensions.Options;
using MVC.Auth.Jwt;
using MVC.Auth;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly IOptions<JwtBearerSettings> _jwtopt;

    public HomeController(IOptions<JwtBearerSettings> jwtopt)
    {
        _jwtopt = jwtopt;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy(Greet gt)
    {
        gt.sayHello();
        Console.WriteLine(_jwtopt.Value.SigningKey);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
