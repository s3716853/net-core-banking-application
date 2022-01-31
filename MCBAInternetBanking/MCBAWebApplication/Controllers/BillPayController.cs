using MCBABackend.Models;
using MCBABackend.Utilities;
using MCBAWebApplication.Models.ViewModels;
using MCBAWebApplication.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MCBAWebApplication.Controllers;

public class BillPayController : McbaController
{
    public BillPayController(ILogger<DepositController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: BillPay/");
        ViewBag.AccountList = await GetAccountList();
        ViewBag.BillPayList = await GetBillPayList();
        ViewBag.PayeeList = await GetPayeeList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm([FromForm] DepositWithdrawViewModel depositWithdrawViewModel)
    {
        _logger.LogInformation("POST: BillPay/Confirm");

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
        _logger.LogInformation("POST: BillPay/Complete");
        
        await CheckViewModel(depositWithdrawViewModel);
        if (!ModelState.IsValid) return View("Index", depositWithdrawViewModel);

        await PutQueryCustomerApi($"{_connectionString}/Transaction/Withdraw", new
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
        List<string> accountNumbers = new List<string>();

        foreach (Account customerAccount in customer.Accounts)
        {
            accountNumbers.Add(customerAccount.AccountNumber);
        }

        return accountNumbers;
    }

    private async Task<List<BillPay>> GetBillPayList()
    {
        Customer customer = await GetLoggedInCustomer();
        List<BillPay> billPayList = new List<BillPay>();

        // TODO

        return billPayList;
    }

    private async Task<List<Payee>> GetPayeeList()
    {
        // TODO

        return new List<Payee>();
    }

}

