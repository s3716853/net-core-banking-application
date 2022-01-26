using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;
using MCBAWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace MCBAWebApplication.Controllers;

public abstract class McbaController : Controller
{
    protected readonly ILogger<McbaController> _logger;
    protected readonly IServiceProvider _serviceProvider;
    protected readonly string _connectionString;
    protected McbaController(ILogger<McbaController> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _connectionString = configuration.GetConnectionString("CustomerApi");
    }

    // Check login before every controller action, redirect if not logged in to login
    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
        if (ctx.HttpContext.Session.GetString(nameof(Customer.CustomerID)) == null)
        {
            ctx.Result = RedirectToAction("Index", "Login");
        }
        base.OnActionExecuting(ctx);
    }
}

