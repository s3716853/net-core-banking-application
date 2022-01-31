using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BillPayController : McbaController<BillPay, BillPayRepository, int>
{
    public BillPayController(BillPayRepository repo, ILogger<BillPay> logger) : base(repo, logger)
    {
    }
}
