using MCBABackend.Contexts;
using MCBABackend.Models;

namespace MCBACustomerApi.Repositories;

public class LoginRepository : DataRepository<Login, string>
{
    public LoginRepository(McbaContext context) : base(context)
    {
    }

    public override IEnumerable<Login> GetAll()
    {
        return _context.Login.ToList();
    }

    public override Login? Get(string id)
    {
        return _context.Login.Find(id);
    }
}
