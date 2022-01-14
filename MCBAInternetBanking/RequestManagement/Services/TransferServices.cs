using MCBABackend.Managers;
using MCBABackend.Models;

namespace MCBABackend.Services;

public static class TransferServices
{
    public static string? Transfer(Account accountTo, Account accountFrom, decimal amount, string? comment)
    {
        try
        {
            DatabaseManager.Transfer(accountTo, accountFrom, amount, comment);
            return null;
        }
        catch (ArgumentException e)
        {
            return e.Message;
        }
    }
}