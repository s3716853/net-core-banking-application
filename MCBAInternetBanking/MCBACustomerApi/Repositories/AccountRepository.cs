using MCBABackend.Contexts;
using MCBABackend.Models;

namespace MCBACustomerApi.Repositories;

public class AccountRepository : DataRepository<Account, string>
{
    public AccountRepository(McbaContext context) : base(context)
    {
    }

    public override IEnumerable<Account> GetAll()
    {
        return _context.Account.ToList();
    }

    public override Account? Get(string id)
    {
        return _context.Account.Find(id);
    }
}
