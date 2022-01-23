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
}
