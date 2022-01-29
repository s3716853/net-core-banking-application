using MCBABackend.Models;
using MCBABackend.Repositories;
using MCBABackend.Utilities;
using MCBACustomerApi.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPut]
    [Route("Withdraw")]
    public async Task<StatusCodeResult> Withdraw(ControllerInputs.WithdrawInput input)
    {
        await _repo.Withdraw(new Transaction()
        {
            Amount = input.amount,
            Comment = input.comment,
            OriginAccountNumber = input.accountNumber,
            TransactionType = TransactionType.Withdraw,
            TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        });
        return StatusCode(200);
    }

    [HttpPut]
    [Route("Transfer")]
    public async Task<StatusCodeResult> Transfer(ControllerInputs.TransferInput input)
    {
        await _repo.Transfer(new Transaction()
        {
            Amount = input.amount,
            Comment = input.comment,
            OriginAccountNumber = input.originAccountNumber,
            DestinationAccountNumber = input.destinationAccountNumber,
            TransactionType = TransactionType.Transfer,
            TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        });
        return StatusCode(200);
    }
}
