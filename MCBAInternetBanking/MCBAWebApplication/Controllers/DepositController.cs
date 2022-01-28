using JetBrains.Annotations;
using MCBABackend.Models;
using MCBAWebApplication.Models.ViewModels;
using MCBAWebApplication.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MCBAWebApplication.Controllers;

public class DepositController : McbaController
{
    public DepositController(ILogger<DepositController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }
    public async Task<IActionResult> Index()
    {
        ViewBag.AccountList = await getAccountList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(DepositViewModel depositViewModel)
    {
        _logger.LogInformation("POST: Deposit/");
        await CheckViewModel(depositViewModel);
        if (!ModelState.IsValid)
        {
            ViewBag.AccountList = await getAccountList();
            return View(depositViewModel);
        }
        return RedirectToAction("Confirm", depositViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm([FromForm] DepositViewModel depositViewModel)
    {
        _logger.LogInformation("POST: Deposit/Confirm");
        await CheckViewModel(depositViewModel);
        if (!ModelState.IsValid) return RedirectToAction("Index", depositViewModel);

        return RedirectToAction("Index");
        // return View(depositViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete([FromForm] DepositViewModel depositViewModel){
        _logger.LogInformation("POST: Deposit/Complete");
        await CheckViewModel(depositViewModel);
        if (!ModelState.IsValid) return View("Index", depositViewModel);
        _logger.LogInformation("Confirm has been successful!");
        return RedirectToAction("Index", "Statement");
    }

    private async Task CheckViewModel(DepositViewModel depositViewModel)
    {
        Account? account = await QueryCustomerApi<Account>($"{_connectionString}/Account/{depositViewModel.Account}");
        if (account == null)
        {
            ModelState.AddModelError("", "Account does not exist");
        }
        else
        {
            BankActionValidation.Deposit(ModelState, account, depositViewModel.Amount);
        }
    }

    private async Task<List<string>> getAccountList()
    {
        Customer customer = await GetLoggedInCustomer();
        List<string> accountNumbers = new();

        foreach (Account customerAccount in customer.Accounts)
        {
            accountNumbers.Add(customerAccount.AccountNumber);
        }

        return accountNumbers;
    }

}

