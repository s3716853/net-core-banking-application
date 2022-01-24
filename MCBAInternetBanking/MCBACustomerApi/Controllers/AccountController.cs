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
}
