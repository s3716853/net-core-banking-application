using MCBAWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MCBAWebApplication.Controllers;

public class HomeController : McbaController
{
    public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
