using aspnetcoremvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcoremvc.Controllers;
[Area("ProductManage")]
public class StarController : Controller
{

    private readonly StarService _starService;
    private readonly ILogger<HomeController> _logger;

    public StarController(StarService starService, ILogger<HomeController> logger)
    {
        _starService = starService;
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public IActionResult Detail(int id)
    {
        var result = _starService.stars.Where(star => star.Id == id).FirstOrDefault();
        return View("Detail", result);
    }
}