using MCBABackend.Contexts;
using MCBABackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBABackend.Repositories;

public class LoginRepository : DataRepository<Login, string>
{
    public LoginRepository(McbaContext context) : base(context)
    {
    }

    public override async Task<List<Login>> GetAll()
    {
        return await _context.Login.ToListAsync();
    }

    public override async Task<Login?> Get(string id)
    {
        return await _context.Login.
            FirstOrDefaultAsync(login => login.LoginID == id);
    }

    public override async Task Add(Login entity)
    {
        await _context.Login.AddAsync(entity);
    }

    public override async Task Update(Login entity)
    {
        _context.Login.Update(entity); 
        await _context.SaveChangesAsync();

    }

    public override async Task Delete(string id)
    {
        var entity = await Get(id);
        if (entity != null)
        {
            _context.Login.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Login?> GetByCustomerId(string customerId)
    {
        return await _context.Login.FirstOrDefaultAsync(login => login.CustomerID == customerId);
    }
}
