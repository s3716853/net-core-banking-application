using MCBABackend.Models;

namespace MCBABackend.Managers.Interfaces;

public interface ITransactionManager
{
    public void Insert(Transaction transaction);
}