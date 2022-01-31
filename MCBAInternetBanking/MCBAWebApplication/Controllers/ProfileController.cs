using MCBACommon.Models;
using MCBAWebApplication.Models.ViewModels;
using MCBAWebApplication.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MCBAWebApplication.Controllers;

public class ProfileController : McbaController
{
    public ProfileController(ILogger<DepositController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: Profile/");

        return View(await GetDefaultViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Customer([FromForm] ProfileCustomerViewModel profileCustomerViewModel)
    {
        _logger.LogInformation("POST: Profile/Customer");

        if (!ModelState.IsValid)
        {
            ProfileViewModel profileViewModel = await GetDefaultViewModel();
            profileViewModel.ProfileCustomerViewModel = profileCustomerViewModel;
            return View("Index", profileViewModel);
        }

        await PutQueryCustomerApi($"{_connectionString}/Customer/Update", profileCustomerViewModel);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] ProfileLoginViewModel profileLoginViewModelViewModel)
    {
        _logger.LogInformation("POST: Profile/Login");

        ProfileActionValidation.Login(ModelState, profileLoginViewModelViewModel.PasswordOldHash, profileLoginViewModelViewModel.PasswordOld);

        if (!ModelState.IsValid)
        {
            ProfileViewModel profileViewModel = await GetDefaultViewModel();
            profileViewModel.ProfileLoginViewModel = profileLoginViewModelViewModel;
            return View("Index", profileViewModel);
        }

        await PutQueryCustomerApi($"{_connectionString}/Login/Update", profileLoginViewModelViewModel);

        return RedirectToAction("Index");
    }

    private async Task<ProfileViewModel> GetDefaultViewModel()
    {
        Customer customer = await GetLoggedInCustomer();

        ProfileCustomerViewModel profileCustomerViewModel = new ProfileCustomerViewModel()
        {
            CustomerID = customer.CustomerID,
            Name = customer.Name,
            TFN = customer.TFN,
            Address = customer.Address,
            Suburb = customer.Suburb,
            State = customer.State,
            PostCode = customer.PostCode,
            Mobile = customer.Mobile
        };

        ProfileLoginViewModel profileLoginViewModel = new ProfileLoginViewModel()
        {
            CustomerID = customer.CustomerID,
            PasswordNew = "",
            PasswordOld = "",
            PasswordOldHash = customer.Login.PasswordHash
        };

        return new ProfileViewModel()
        {
            ProfileCustomerViewModel = profileCustomerViewModel,
            ProfileLoginViewModel = profileLoginViewModel
        };
    }

}

