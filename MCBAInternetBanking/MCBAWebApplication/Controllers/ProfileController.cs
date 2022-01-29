using MCBABackend.Models;
using MCBAWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MCBAWebApplication.Controllers;

public class ProfileController : McbaController
{
    public ProfileController(ILogger<DepositController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }
    public async Task<IActionResult> Index(ProfileViewModel? profileViewModel)
    {
        _logger.LogInformation("GET: Profile/");
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

        if (profileViewModel != null)
        {
            profileViewModel.ProfileCustomerViewModel ??= profileCustomerViewModel;
            profileViewModel.ProfileLoginViewModel ??= profileLoginViewModel;

            return View(profileViewModel);
        }

        return View(new ProfileViewModel()
        {
            ProfileLoginViewModel = profileLoginViewModel,
            ProfileCustomerViewModel = profileCustomerViewModel
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Customer([FromForm] ProfileCustomerViewModel profileCustomerViewModel)
    {
        _logger.LogInformation("POST: Profile/Customer");

        if (!ModelState.IsValid)
            return View("Index", new ProfileViewModel()
            {
                ProfileLoginViewModel = null,
                ProfileCustomerViewModel = profileCustomerViewModel
            });

        

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] ProfileLoginViewModel profileLoginViewModelViewModel)
    {
        _logger.LogInformation("POST: Profile/Login");

        if (!ModelState.IsValid)
            return View("Index", new ProfileViewModel()
            {
                ProfileLoginViewModel = profileLoginViewModelViewModel,
                ProfileCustomerViewModel = null
            });
        _logger.LogInformation("NO MODEL ERROR IN Login");
        return RedirectToAction("Index");
    }

}

