using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspnetcoremvc.Models;

namespace aspnetcoremvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    [TempData]
    public string Name { get; set; } = string.Empty;

    [TempData]
    public string Age { get; set; } = string.Empty;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Name = "Try Again";
        Age = "24";

        TempData.Keep("Name");
        TempData.Keep("Age");

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
