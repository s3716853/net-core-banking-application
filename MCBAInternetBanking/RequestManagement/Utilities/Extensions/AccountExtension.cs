using System.Text;
using MCBABackend.Models;
using MCBABackend.Models.Dto;

namespace MCBABackend.Utilities.Extensions;
public static class AccountExtension
{
    // Cannot override ToString with extension methods
    // Chose to use an Extension method instead of implementing an overrided ToString
    // in the model classes as to keep them purely as data classes
    public static string AsString(this Account account)
    {
        string transactionStrings = "";
        account.Transactions.ForEach(transaction =>
        {
            transactionStrings += transaction.AsString();
        });
        return new StringBuilder().AppendArray(new string[]
        {
            $"  AccountNumber={account.AccountNumber}",
            $"  AccountType={account.AccountType}",
            $"  CustomerID={account.CustomerID}",
            "   -Transactions-",
            transactionStrings
        }).ToString();
    }

    public static bool HasFreeTransactions(this Account account)
    {
        return account.Transactions.Count < 2;
    }

    public static Account ToAccount(this AccountDto dto)
    {
        List<Transaction> transactions = new List<Transaction>();
        dto.Transactions.ForEach(transactionDto =>
        {
            transactions.Add(transactionDto.ToTransaction(dto.AccountNumber));
        });
        return new Account()
        {
            AccountNumber = dto.AccountNumber,
            AccountType = dto.AccountType.ToAccountType(),
            CustomerID = dto.CustomerID,
            Transactions = transactions
        };
    }
}
