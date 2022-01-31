using MCBABackend.Contexts;
using MCBABackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBABackend.Repositories;

public class PayeeRepository : DataRepository<Payee, int>
{
    public PayeeRepository(McbaContext context) : base(context)
    {
    }

    public override async Task<List<Payee>> GetAll()
    {
        return await _context.Payee.ToListAsync();
    }

    public override async Task<Payee?> Get(int id)
    {
        return await _context.Payee.
            FirstOrDefaultAsync(payee => payee.PayeeId == id);
    }

    public override async Task Add(Payee entity)
    {
        await _context.Payee.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public override async Task Update(Payee entity)
    {
        _context.Payee.Update(entity);
        await _context.SaveChangesAsync();
    }

    public override async Task Delete(int id)
    {
        var entity = await Get(id);
        if (entity != null)
        {
            _context.Payee.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

