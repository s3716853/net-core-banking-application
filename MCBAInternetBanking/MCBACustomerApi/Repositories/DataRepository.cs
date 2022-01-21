using MCBABackend.Contexts;

namespace MCBACustomerApi.Repositories;

public abstract class DataRepository<TEntity, TKey> where TEntity : class
{
    protected readonly McbaContext _context;

    protected DataRepository(McbaContext context)
    {
        _context = context;
    }

    public abstract IEnumerable<TEntity> GetAll();
    public abstract TEntity? Get(TKey id);
}