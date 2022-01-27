using MCBABackend.Contexts;
using MCBABackend.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace MCBABackend.Repositories;

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

    public async Task<List<Transaction>> GetByAccountNumber(string accountNumber)
    {
        return await _context.Transaction.Where(transaction => transaction.OriginAccountNumber == accountNumber)
            .ToListAsync();
    }
}
