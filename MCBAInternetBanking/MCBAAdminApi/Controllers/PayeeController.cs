using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PayeeController : McbaController<Payee, PayeeRepository, int>
{
    public PayeeController(PayeeRepository repo, ILogger<Payee> logger) : base(repo, logger)
    {
    }
}
