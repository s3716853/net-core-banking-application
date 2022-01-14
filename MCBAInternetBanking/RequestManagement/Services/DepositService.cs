using MCBABackend.Contexts;
using MCBABackend.Managers;
using MCBABackend.Models;

namespace MCBABackend.Services;

public static class DepositService
{
    public static void Deposit(Account account, decimal amount, string? comment)
    {
        comment = comment.Length > 0 ? comment : null;
        DatabaseManager.Deposit(account, amount, comment);
    }

    // Retrieves logged in user's accounts
    public static List<Account> RetrieveAccounts()
    {
        return DatabaseManager.RetrieveUserAccounts(UserContext.GetInstance().CustomerId);
    }
}