using MCBACommon.Models;
using MCBAWebApplication.Models.ViewModels;
using MCBAWebApplication.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MCBAWebApplication.Controllers;

public class TransferController : McbaController
{
    public TransferController(ILogger<DepositController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: Transfer/");
        ViewBag.AccountList = await GetAccountList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm([FromForm] TransferViewModel transferViewModel)
    {
        _logger.LogInformation("POST: Transfer/Confirm");
        await CheckViewModel(transferViewModel);
        ViewBag.AccountList = await GetAccountList();
        if (!ModelState.IsValid)
        {
            return View("Index", transferViewModel);
        }

        return View(transferViewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete([FromForm] TransferViewModel transferViewModel){
        _logger.LogInformation("POST: Transfer/Complete");
        
        await CheckViewModel(transferViewModel);
        if (!ModelState.IsValid) return View("Index", transferViewModel);
        
        await PostQueryCustomerApi($"{_connectionString}/Transaction/Transfer", new
        {
            comment = transferViewModel.Comment,
            amount = transferViewModel.Amount,
            originAccountNumber = transferViewModel.OriginAccount,
            destinationAccountNumber = transferViewModel.DestinationAccount
        });

        return RedirectToAction("Index", "Statement");
    }

    private async Task CheckViewModel(TransferViewModel transferViewModel)
    {
        if (ModelState.IsValid)
        {
            Account? accountOrigin = await GetQueryCustomerApi<Account>($"{_connectionString}/Account/{transferViewModel.OriginAccount}");
            Account? accountDestination = await GetQueryCustomerApi<Account>($"{_connectionString}/Account/{transferViewModel.DestinationAccount}");
            bool accountsExist = true;
            if (accountOrigin == null)
            {
                accountsExist = false;
                ModelState.AddModelError("", "Origin account does not exist");
            }
            if (accountDestination == null)
            {
                accountsExist = false;
                ModelState.AddModelError("", "Destination account does not exist");
            }
            if(accountsExist)
            {
                // supressing nullable as none of these should be null by the time we reach this point
                BankActionValidation.Transfer(ModelState, accountOrigin!, transferViewModel.Amount, HttpContext.Session.GetString(nameof(Customer.CustomerID))!);
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

