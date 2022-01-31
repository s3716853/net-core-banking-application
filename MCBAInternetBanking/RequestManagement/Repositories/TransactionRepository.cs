using MCBACommon.Contexts;
using MCBACommon.Models;
using MCBACommon.Utilities;
using MCBACommon.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MCBACommon.Repositories;

public class TransactionRepository : DataRepository<Transaction, int>
{
    public TransactionRepository(McbaContext context) : base(context)
    {
    }

    public override async Task<List<Transaction>> GetAll()
    {
        return await _context.Transaction.ToListAsync();
    }

    public override async Task<Transaction?> Get(int id)
    {
        return await _context.Transaction.
            FirstOrDefaultAsync(transaction => transaction.TransactionID == id);
    }

    public override async Task<int> Add(Transaction entity)
    {
        await _context.Transaction.AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    public override async Task Update(Transaction entity)
    {
        _context.Transaction.Update(entity);
        await _context.SaveChangesAsync();
    }


    public override async Task Delete(int id)
    {
        var entity = await Get(id);
        if (entity != null)
        {
            _context.Transaction.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Transaction>> GetByAccountNumber(string accountNumber)
    {
        return await _context.Transaction.Where(transaction => transaction.OriginAccountNumber == accountNumber)
            .ToListAsync();
    }

    public async Task<List<Transaction>> GetByAccountNumberWithRange(string accountNumber, DateTime? start, DateTime? end)
    {
        if (start == null || end == null)
        {
            return await GetByAccountNumber(accountNumber);
        }
        else
        {
            return await _context.Transaction.
                Where(transaction => transaction.OriginAccountNumber == accountNumber).
                Where(transaction => transaction.TransactionTimeUtc >= start).
                Where(transaction => transaction.TransactionTimeUtc <= end).ToListAsync();
        }
    }

    public async Task Deposit(Transaction entity)
    {
        DateTime transactionTime = DateTime.Now.ToUniversalTime();
        Account? account = await _context.Account.Include(account => account.Transactions)
            .FirstOrDefaultAsync(account => account.AccountNumber == entity.OriginAccountNumber);

        if (account != null)
        {
            entity.TransactionTimeUtc = transactionTime;
            await Add(entity);
        }

    }

    public async Task Withdraw(Transaction entity)
    {
        DateTime transactionTime = DateTime.Now.ToUniversalTime();
        Account? account = await _context.Account.
            Include(account => account.Transactions).
            FirstOrDefaultAsync(account => account.AccountNumber == entity.OriginAccountNumber);
            
        if (account != null)
        {
            entity.TransactionTimeUtc = transactionTime; // Want to be sure that all transactions are treated as if they happend at the same time
            await Add(entity);
            if (!account.HasFreeTransactions())
            {
                // If no free transactions then time to charge
                await Add(new Transaction()
                {
                    Amount = Constants.WithdrawTransactionFee,
                    Comment = Constants.WithdrawFeeComment,
                    OriginAccountNumber = account.AccountNumber,
                    TransactionTimeUtc = transactionTime,
                    TransactionType = TransactionType.Service
                });
            }
        }
    }

    // Transaction is from the Origin account's perspective 
    public async Task Transfer(Transaction entity)
    {
        Account? accountOrigin = await _context.Account.Include(account => account.Transactions)
            .FirstOrDefaultAsync(account => account.AccountNumber == entity.OriginAccountNumber);

        Account? accountDestination = await _context.Account.Include(account => account.Transactions)
            .FirstOrDefaultAsync(account => account.AccountNumber == entity.DestinationAccountNumber);

        if (accountOrigin != null && accountDestination != null)
        {
            DateTime transactionTime = DateTime.Now.ToUniversalTime();
            
            entity.TransactionTimeUtc = transactionTime; // Want to be sure that all transactions are treated as if they happend at the same time
            await Add(entity); // this is the transaction that will be stored on the ORIGIN account
            await Add(new Transaction() // this is the transaction that will be stored on the DESTINATION account
            {
                Amount = entity.Amount,
                Comment = entity.Comment,
                OriginAccountNumber = accountDestination.AccountNumber!, // No transaction (should) be passed here that has a null destination (not a transfer)
                TransactionType = TransactionType.Transfer,
                TransactionTimeUtc = transactionTime
            });

            if (!accountOrigin.HasFreeTransactions())
            {
                await Add(new Transaction() // The service fee!
                {
                    Amount = Constants.TransferTransactionFee,
                    Comment = Constants.TransferFeeComment,
                    OriginAccountNumber = accountOrigin.AccountNumber,
                    TransactionType = TransactionType.Service,
                    TransactionTimeUtc = transactionTime
                });
            }
        }
    }
}
