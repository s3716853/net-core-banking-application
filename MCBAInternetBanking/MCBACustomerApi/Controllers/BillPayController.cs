using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using MCBACustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MCBACustomerApi.Controllers;

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

    [HttpPost]
    [Route("New")]
    public async Task<StatusCodeResult> New(ControllerInputs.BillPayCreateInput input)
    {
        await _repo.Add(new BillPay()
        {
            AccountNumber = input.Account,
            Amount = input.Amount,
            Completed = false,
            PayeeId = input.Payee,
            Period = input.Period,
            ScheduleTimeUtc = input.ScheduleTimeUtc.ToUniversalTime() //Be sure its actually been sent as universal time
        });

        return StatusCode(200);
    }
}
