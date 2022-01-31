using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : McbaController<Customer, CustomerRepository, string>
{
    public CustomerController(CustomerRepository repo, ILogger<Customer> logger) : base(repo, logger)
    {
    }
}
