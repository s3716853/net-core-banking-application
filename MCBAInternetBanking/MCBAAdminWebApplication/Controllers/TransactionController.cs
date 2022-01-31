using MCBAAdminWebApplication.Models.ViewModels;
using MCBACommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminWebApplication.Controllers;

public class TransactionController : McbaController
{
    public TransactionController(ILogger<CustomerController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: Transaction/");

        List<Account>? accounts = await GetQueryApi<List<Account>>($"{_connectionString}/Account");

        return View(accounts);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id, [FromQuery] string? startDate, [FromQuery] string? endDate)
    {
        _logger.LogInformation("GET: Transaction/Details");

        string queryString = "";

        if (startDate != null && endDate != null)
        {
            queryString = $"?startDate={startDate}&endDate={endDate}";
        }

        List<Transaction>? transactions = await GetQueryApi<List<Transaction>>($"{_connectionString}/Transaction/Account/{id}{queryString}");

        ViewBag.Transactions = transactions;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(ProfileCustomerViewModel profileCustomerViewModel)
    {
        _logger.LogError("POST: Transaction/");

        if (!ModelState.IsValid)
        {
            return View("Index", profileCustomerViewModel);
        }

        await PutQueryApi($"{_connectionString}/Customer/Update", profileCustomerViewModel);

        return RedirectToAction("Index");
    }
}

