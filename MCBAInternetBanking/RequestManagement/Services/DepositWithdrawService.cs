using MCBABackend.Contexts;
using MCBABackend.Managers;
using MCBABackend.Models;

namespace MCBABackend.Services;

public static class DepositWithdrawService
{
    public static void Deposit(Account account, decimal amount, string? comment)
    {
        comment = comment.Length > 0 ? comment : null;
        DatabaseManager.Deposit(account, amount, comment);
    }

    public static void Withdraw(Account account, decimal amount, string? comment)
    {
        comment = comment.Length > 0 ? comment : null;
        DatabaseManager.Withdraw(account, amount, comment);
    }
}