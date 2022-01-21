using MCBABackend.Contexts;
using MCBABackend.Models;

namespace MCBACustomerApi.Repositories;

public class TransactionRepository : DataRepository<Transaction, int>
{
    public TransactionRepository(McbaContext context) : base(context)
    {
    }

    public override IEnumerable<Transaction> GetAll()
    {
        return _context.Transaction.ToList();
    }

    public override Transaction? Get(int id)
    {
        return _context.Transaction.Find(id);
    }
}
