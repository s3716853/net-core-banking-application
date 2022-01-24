using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;
using MCBAWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MCBAWebApplication.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _connectionString;
    public LoginController(ILogger<LoginController> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _connectionString = $"{configuration.GetConnectionString("CustomerApi")}/Login";
    }
    // GET: Login/
    public ActionResult Index()
    {
        _logger.LogInformation("GET Login/");
        if (HttpContext.Session.GetString(nameof(Customer.CustomerID)) != null)
        {
            _logger.LogInformation($"Already logged in as {HttpContext.Session.GetString(nameof(Customer.CustomerID))}");
            // Go Home if already logged in
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    // POST: Login/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Index(LoginViewModel loginViewModel)
    {
        _logger.LogInformation("POST Login/");
        if (!ModelState.IsValid) return View(loginViewModel);

        HttpClient? httpClient = _serviceProvider.GetService<HttpClient>();

        //Supressing nullable as _connectionString is never null as it is set in the constructor
        string loginObjectgResponse =
            await httpClient?.GetStringAsync($"{_connectionString}/{loginViewModel.LoginId}")!; 
        
        Login? login = JsonConvert.DeserializeObject<Login>(loginObjectgResponse);

        if (login != null && login.Verify(loginViewModel.Password))
        {
            HttpContext.Session.SetString(nameof(Customer.CustomerID), login.CustomerID);
            return RedirectToAction("Index", "Home");
        }
        
        // if the code has made this far then the login details are wrong
        ModelState.AddModelError("", "Incorrect password or account doesn't exist");
        return View();
    }

    // GET: Login/Logout
    [HttpGet]
    public ActionResult Logout()
    {
        _logger.LogInformation("GET Login/Logout");
        HttpContext.Session.Remove(nameof(Customer.CustomerID));
        return RedirectToAction("Index", "Login");
    }
}
