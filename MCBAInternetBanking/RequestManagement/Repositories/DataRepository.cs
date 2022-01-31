using MCBACommon.Contexts;

namespace MCBACommon.Repositories;

public abstract class DataRepository<TEntity, TKey> where TEntity : class
{
    protected readonly McbaContext _context;

    protected DataRepository(McbaContext context)
    {
        _context = context;
    }

    public abstract Task<List<TEntity>> GetAll();
    public abstract Task<TEntity?> Get(TKey id);
    public abstract Task Add(TEntity entity);
    public abstract Task Update(TEntity entity);
    public abstract Task Delete(TKey id);
}