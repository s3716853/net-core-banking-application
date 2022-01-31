using MCBABackend.Contexts;
using MCBABackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBABackend.Repositories;

public class BillPayRepository : DataRepository<BillPay, int>
{
    public BillPayRepository(McbaContext context) : base(context)
    {
    }

    public override async Task<List<BillPay>> GetAll()
    {
        return await _context.BillPay.
            Include(billPay => billPay.Payee).
            Include(billPay => billPay.Account).
            Include(billPay => billPay.Account.Transactions).ToListAsync();
    }

    public override async Task<BillPay?> Get(int id)
    {
        return await _context.BillPay.
            Include(billPay => billPay.Payee).
            Include(billPay => billPay.Account).
            FirstOrDefaultAsync(billPay => billPay.BillPayId == id);
    }

    public override async Task Add(BillPay entity)
    {
        await _context.BillPay.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public override async Task Update(BillPay entity)
    {
        _context.BillPay.Update(entity);
        await _context.SaveChangesAsync();
    }

    public override async Task Delete(int id)
    {
        var entity = await Get(id);
        if (entity != null)
        {
            _context.BillPay.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<BillPay>> GetByCustomerId(string customerId)
    {
        return await _context.BillPay.Include(billPay => billPay.Payee).Include(billPay => billPay.Account)
            .Where(billPay => billPay.Account.CustomerID == customerId).ToListAsync();
    }
}

