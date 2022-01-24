using MCBABackend.Contexts;
using MCBABackend.Models;

namespace MCBABackend.Repositories;

public abstract class DataRepository<TEntity, TKey> where TEntity : class
{
    protected readonly McbaContext _context;

    protected DataRepository(McbaContext context)
    {
        _context = context;
    }

    public abstract Task<List<TEntity>> GetAll();
    public abstract Task<TEntity?> Get(TKey id);
}