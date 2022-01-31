using MCBAAdminWebApplication.Models.ViewModels;
using MCBACommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminWebApplication.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _connectionString;
    public LoginController(ILogger<LoginController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base()
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _connectionString = $"{configuration.GetConnectionString("CustomerApi")}/Login";
    }
    // GET: Login/
    public ActionResult Index()
    {
        _logger.LogInformation("GET Login/");
        if (HttpContext.Session.GetString("LoggedIn") != null)
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

        if (loginViewModel.Password == "admin" && loginViewModel.LoginId == "admin")
        {
            HttpContext.Session.SetString("LoggedIn", "true");
            return RedirectToAction("Index", "Home");
        }
        
        // if the code has made this far then the login details are wrong
        ModelState.AddModelError("", "Incorrect password or username");
        return View();
    }

    // GET: Login/Logout
    [HttpGet]
    public ActionResult Logout()
    {
        _logger.LogInformation("GET Login/Logout");
        HttpContext.Session.Remove("LoggedIn");
        return RedirectToAction("Index", "Login");
    }
}
