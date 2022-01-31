using MCBACommon.Controllers;
using MCBACommon.Models;
using MCBACommon.Repositories;
using MCBACommon.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MCBAAdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : McbaController<Transaction, TransactionRepository, int>
{
    public TransactionController(TransactionRepository repo, ILogger<Transaction> logger) : base(repo, logger)
    {

    }

    [HttpGet]
    [Route("Account/{accountNumber}")]
    public async Task<List<Transaction>> GetByRangeAccountNumber(string accountNumber, [FromQuery] string? startDate, [FromQuery] string? endDate)
    {
        DateTime? startDateTime = startDate == null ? null : DateTime.Parse(startDate);
        DateTime? endDateTime = endDate == null ? null : DateTime.Parse(endDate);

        return await _repo.GetByAccountNumberWithRange(accountNumber, startDateTime, endDateTime);
    }
}
