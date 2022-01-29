using System.Linq;
using MCBABackend.Contexts;
using MCBABackend.Models;
using MCBABackend.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MCBABackend.Repositories;

public class AccountRepository : DataRepository<Account, string>
{
    public AccountRepository(McbaContext context) : base(context)
    {
    }

    public override async Task<List<Account>> GetAll()
    {
        return await _context.Account.Include(account => account.Transactions).ToListAsync();
    }

    public override async Task<Account?> Get(string id)
    {
        return await _context.Account.
            Include(account => account.Transactions).
            FirstOrDefaultAsync(account => account.AccountNumber == id);
    }

    public override async Task Add(Account entity)
    {
        await _context.Account.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public override async Task Update(Account entity)
    {
        _context.Account.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Account>> GetByCustomerId(string customerId)
    {
        return await _context.Account.Where(account => account.CustomerID == customerId).Include(account => account.Transactions).ToListAsync();
    }
}
