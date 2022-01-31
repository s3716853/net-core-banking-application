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
    public async Task<List<Transaction>> GetByAccountNumber(string accountNumber)
    {
        return await _repo.GetByAccountNumber(accountNumber);
    }
}
