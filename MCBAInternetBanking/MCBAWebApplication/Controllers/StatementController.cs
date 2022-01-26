using MCBABackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MCBAWebApplication.Controllers;

public class StatementController : McbaController
{
    public StatementController(ILogger<McbaController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: Statement/");
        // the parent class checks if logged in before every action, so this will never be null here
        string? customerId = HttpContext.Session.GetString(nameof(Customer.CustomerID));

        List<Account>? accounts = await QueryCustomerApi<List<Account>>($"{_connectionString}/Account/Customer/{customerId}");

        return View(accounts);
    }

    [HttpGet]
    public async Task<IActionResult> Account(string id)
    {
        _logger.LogInformation($"GET: Statement/{id}");
        Account? account = await QueryCustomerApi<Account>($"{_connectionString}/Account/{id}");
        if (account != null)
        {
            account.Transactions = account.Transactions.OrderByDescending(transaction => transaction.TransactionTimeUtc).ToList();
            return View(account);
        }
        else
        {
            return RedirectToAction("Index");
        }
    }
}

