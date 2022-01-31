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

    [HttpGet]
    [Route("Customer/{customerId}")]
    public async Task<List<BillPay>> GetByCustomerId(string customerId)
    {
        return await _repo.GetByCustomerId(customerId);
    }
}
