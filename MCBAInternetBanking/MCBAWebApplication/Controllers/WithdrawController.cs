using MCBACommon.Models;
using MCBAWebApplication.Models.ViewModels;
using MCBAWebApplication.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MCBAWebApplication.Controllers;

public class WithdrawController : McbaController
{
    public WithdrawController(ILogger<DepositController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: Withdraw/");
        ViewBag.AccountList = await GetAccountList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm([FromForm] DepositWithdrawViewModel depositWithdrawViewModel)
    {
        _logger.LogInformation("POST: Withdraw/Confirm");

        ViewBag.AccountList = await GetAccountList();
        await CheckViewModel(depositWithdrawViewModel);
        if (!ModelState.IsValid)
        {
            return View("Index", depositWithdrawViewModel);
        }

        return View(depositWithdrawViewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete([FromForm] DepositWithdrawViewModel depositWithdrawViewModel){
        _logger.LogInformation("POST: Withdraw/Complete");
        
        await CheckViewModel(depositWithdrawViewModel);
        if (!ModelState.IsValid) return View("Index", depositWithdrawViewModel);

        await PostQueryCustomerApi($"{_connectionString}/Transaction/Withdraw", new
        {
            comment = depositWithdrawViewModel.Comment,
            amount = depositWithdrawViewModel.Amount,
            accountNumber = depositWithdrawViewModel.Account,
        });

        return RedirectToAction("Index", "Statement");
    }

    private async Task CheckViewModel(DepositWithdrawViewModel depositWithdrawViewModel)
    {
        if (ModelState.IsValid)
        {
            Account? account = await GetQueryCustomerApi<Account>($"{_connectionString}/Account/{depositWithdrawViewModel.Account}");
            if (account == null)
            {
                ModelState.AddModelError("", "Account does not exist");
            }
            else
            {
                BankActionValidation.Withdraw(ModelState, account, depositWithdrawViewModel.Amount);
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

