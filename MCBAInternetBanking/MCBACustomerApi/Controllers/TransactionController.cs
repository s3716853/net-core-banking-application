using MCBABackend.Models;
using MCBABackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MCBACustomerApi.Controllers;

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
