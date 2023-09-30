using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BasicAuth.Models;
using Auth;

namespace BasicAuth.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        return View();
    }

    [BasicAutherization]
    public IActionResult Privacy()
    {

        Console.WriteLine("---------------");
        foreach (var c in HttpContext.User.Claims)
        {
            Console.WriteLine(c.Value);
        };
        Console.WriteLine("---------------");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
