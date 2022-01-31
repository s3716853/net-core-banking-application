using MCBAAdminWebApplication.Models.ViewModels;
using MCBACommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminWebApplication.Controllers;

public class CustomerController : McbaController
{
    public CustomerController(ILogger<CustomerController> logger, IServiceProvider serviceProvider, IConfiguration configuration) : base(logger, serviceProvider, configuration)
    {
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: Customer/");

        List<Customer>? customers = await GetQueryApi<List<Customer>>($"{_connectionString}/Customer");

        return View(customers);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        _logger.LogInformation("GET: Customer/");

        Customer? customer = await GetQueryApi<Customer>($"{_connectionString}/Customer/{id}");

        if (customer == null)
        {
            return Redirect("/");
        }

        ProfileCustomerViewModel profileCustomerViewModel = new ProfileCustomerViewModel()
        {
            Address = customer.Address,
            CustomerID = id,
            Mobile = customer.Mobile,
            Name = customer.Name,
            PostCode = customer.PostCode,
            State = customer.State,
            Suburb = customer.Suburb,
            TFN = customer.TFN
        };

        return View(profileCustomerViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(ProfileCustomerViewModel profileCustomerViewModel)
    {
        _logger.LogError("POST: Customer/");

        if (!ModelState.IsValid)
        {
            return View("Index", profileCustomerViewModel);
        }

        await PutQueryApi($"{_connectionString}/Customer/Update", profileCustomerViewModel);

        return RedirectToAction("Index");
    }
}

