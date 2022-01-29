using JetBrains.Annotations;
using MCBABackend.Models;
using MCBABackend.Utilities;
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
        _logger.LogInformation("GET: Deposit/");
        ViewBag.AccountList = await GetAccountList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm([FromForm] DepositViewModel depositViewModel)
    {
        _logger.LogInformation("POST: Deposit/Confirm");

        ViewBag.AccountList = await GetAccountList();
        await CheckViewModel(depositViewModel);
        if (!ModelState.IsValid)
        {
            return View("Index", depositViewModel);
        }

        return View(depositViewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete([FromForm] DepositViewModel depositViewModel){
        _logger.LogInformation("POST: Deposit/Complete");
        
        await CheckViewModel(depositViewModel);
        if (!ModelState.IsValid) return View("Index", depositViewModel);

        await PutQueryCustomerApi($"{_connectionString}/Transaction", new Transaction()
        {
            Comment = depositViewModel.Comment,
            Amount = depositViewModel.Amount,
            OriginAccountNumber = depositViewModel.Account,
            TransactionType = TransactionType.Deposit,
            TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        });

        return RedirectToAction("Index", "Statement");
    }

    private async Task CheckViewModel(DepositViewModel depositViewModel)
    {
        if (ModelState.IsValid)
        {
            Account? account = await GetQueryCustomerApi<Account>($"{_connectionString}/Account/{depositViewModel.Account}");
            if (account == null)
            {
                ModelState.AddModelError("", "Account does not exist");
            }
            else
            {
                BankActionValidation.Deposit(ModelState, account, depositViewModel.Amount);
            }
        }
    }

    private async Task<List<string>> GetAccountList()
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

