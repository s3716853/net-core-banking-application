using MCBACommon.Models;
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
        await PrepareViewBagForView();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([FromForm] BillPayViewModel billPayViewModel)
    {
        _logger.LogInformation("POST: BillPay/");
        
        await CheckViewModel(billPayViewModel);
        
        if (!ModelState.IsValid)
        {
            await PrepareViewBagForView();
            return View(billPayViewModel);
        }
        
        await PostQueryCustomerApi($"{_connectionString}/BillPay/New", billPayViewModel);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel([FromForm] int billPayId)
    {
        await DeleteQueryCustomerApi($"{_connectionString}/BillPay?key={billPayId}");
        return RedirectToAction("Index");
    }
    private async Task CheckViewModel(BillPayViewModel billPayViewModel)
    {
        if (ModelState.IsValid)
        {
            Account? account = await GetQueryCustomerApi<Account>($"{_connectionString}/Account/{billPayViewModel.Account}");
            if (account == null)
            {
                ModelState.AddModelError("", "Account does not exist");
            }
            else
            {
                BankActionValidation.BillPay(ModelState, account, billPayViewModel.Amount);
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
        return (await GetQueryCustomerApi<List<BillPay>>($"{_connectionString}/BillPay/Customer/{customer.CustomerID}"))!;
    }

    private async Task<List<Payee>> GetPayeeList()
    {
        return (await GetQueryCustomerApi<List<Payee>>($"{_connectionString}/Payee"))!;
    }

    private async Task PrepareViewBagForView()
    {
        ViewBag.AccountList = await GetAccountList();
        ViewBag.BillPayList = await GetBillPayList();
        ViewBag.PayeeList = await GetPayeeList();
    }

}

