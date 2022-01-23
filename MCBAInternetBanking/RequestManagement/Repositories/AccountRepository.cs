using MCBABackend.Contexts;
using MCBABackend.Models;
using Microsoft.EntityFrameworkCore;

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
}
