using MCBABackend.Models;
using MCBABackend.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace MCBACustomerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : McbaController<Account, AccountRepository, string>
{
    public AccountController(AccountRepository repo, ILogger<Account> logger) : base(repo, logger)
    {
    }

    [HttpGet]
    [Route("Customer/{customerId}")]
    public async Task<List<Account>> GetByCustomerId(string customerId)
    {
        return await _repo.GetByCustomerId(customerId);
    }
}
