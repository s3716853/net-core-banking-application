using MCBAAdminWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MCBAAdminWebApplication.Controllers;

public class HomeController : McbaController
{
    public HomeController(ILogger<McbaController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
