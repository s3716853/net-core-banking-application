using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : McbaController<Account, AccountRepository, string>
{
    public AccountController(AccountRepository repo, ILogger<Account> logger) : base(repo, logger)
    {
    }
}
