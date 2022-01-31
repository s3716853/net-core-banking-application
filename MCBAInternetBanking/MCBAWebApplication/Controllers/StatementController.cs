using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MCBACommon.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using X.PagedList;

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

        List<Account>? accounts = await GetQueryCustomerApi<List<Account>>($"{_connectionString}/Account/Customer/{customerId}");

        return View(accounts);
    }

    [HttpGet]
    public async Task<IActionResult> Account(string id, int page = 1)
    {
        _logger.LogInformation($"GET: Statement/{id}?page={page}");

        List<Transaction>? transactions =
            await GetQueryCustomerApi<List<Transaction>>(
                $"{_connectionString}/Transaction/Account/{id}");

        if (transactions != null)
        {
            ViewBag.AccountNumber = id;
            const int pageSize = 4;
            var transactionsPaged = await transactions.OrderByDescending(transaction => transaction.TransactionID)
                .ToPagedListAsync(page, pageSize);
            return View(transactionsPaged);
        }
        else
        {
            return RedirectToAction("Index");
        }

    }
}

