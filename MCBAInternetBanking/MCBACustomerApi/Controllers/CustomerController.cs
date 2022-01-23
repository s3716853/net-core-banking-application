using MCBABackend.Models;
using MCBABackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MCBACustomerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : McbaController<Customer, CustomerRepository, string>
{
    public CustomerController(CustomerRepository repo, ILogger<Customer> logger) : base(repo, logger)
    {
    }
}
