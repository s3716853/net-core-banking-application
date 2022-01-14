using MCBABackend.Models;

namespace MCBABackend.Managers.Interfaces;

public interface IAccountManager
{
    public void Insert(Account account);
    public List<Account> RetrieveUserAccounts(int customerId);
    public void Update(Account account);
}