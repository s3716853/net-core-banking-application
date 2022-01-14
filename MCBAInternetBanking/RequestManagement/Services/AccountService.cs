using MCBABackend.Contexts;
using MCBABackend.Managers;
using MCBABackend.Models;

namespace MCBABackend.Services;

public class AccountService
{
    // Retrieves logged in user's accounts
    public static List<Account> RetrieveAccounts()
    {
        return DatabaseManager.RetrieveUserAccounts(UserContext.GetInstance().CustomerId);
    }

    public static Account? RetrieveAccount(int accountNumber)
    {
        return DatabaseManager.RetrieveAccount(accountNumber);
    }
}