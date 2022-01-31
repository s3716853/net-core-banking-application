using MCBABackend.Models;
using MCBABackend.Repositories;
using MCBABackend.Utilities;
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

    [HttpPut]
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
            ScheduleTimeUtc = input.ScheduleTimeUtc
        });

        return StatusCode(200);
    }
}
