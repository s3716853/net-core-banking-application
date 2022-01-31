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
        return RedirectToAction("Index", "Statement"); ;
    }
}
