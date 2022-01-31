using System.Diagnostics;
using MCBAAdminWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace MCBAAdminWebApplication.Controllers;

public abstract class McbaController : Controller
{
    protected readonly ILogger<McbaController> _logger;
    protected readonly IServiceProvider _serviceProvider;
    protected readonly string _connectionString;
    protected McbaController(ILogger<McbaController> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _connectionString = configuration.GetConnectionString("AdminApi");
    }

    // Check login before every controller action, redirect if not logged in to login
    public override void OnActionExecuting(ActionExecutingContext ctx)
    {
        if (ctx.HttpContext.Session.GetString("LoggedIn") == null)
        {
            ctx.Result = RedirectToAction("Index", "Login");
        }
        base.OnActionExecuting(ctx);
    }

    protected async Task<T?> GetQueryApi<T>(string connectionString)
    {
        HttpClient? httpClient = _serviceProvider.GetService<HttpClient>();

        //Supressing nullable as _connectionString is never null as it is set in the constructor
        string response =
            await httpClient?.GetStringAsync($"{connectionString}")!;

        return JsonConvert.DeserializeObject<T>(response);
    }

    protected async Task<HttpResponseMessage> PutQueryApi(string connectionString, object objectToSend)
    {
        HttpClient? httpClient = _serviceProvider.GetService<HttpClient>();
        return await httpClient?.PutAsJsonAsync(new Uri(connectionString), objectToSend)!;
    }

    protected async Task<HttpResponseMessage> DeleteQueryApi(string connectionString)
    {
        HttpClient? httpClient = _serviceProvider.GetService<HttpClient>();
        return await httpClient?.DeleteAsync(new Uri(connectionString))!;
    }

    protected async Task<HttpResponseMessage> PostQueryApi(string connectionString, object objectToSend)
    {
        HttpClient? httpClient = _serviceProvider.GetService<HttpClient>();
        return await httpClient?.PostAsJsonAsync(new Uri(connectionString), objectToSend)!;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

