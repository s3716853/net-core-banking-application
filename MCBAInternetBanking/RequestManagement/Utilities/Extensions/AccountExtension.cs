using System.Text;
using RequestManagement.Models;

namespace RequestManagement.Utilities.Extensions
{
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
                $"AccountNumber={account.AccountNumber}",
                $"AccountType={account.AccountType}",
                $"Balance={account.Balance}",
                $"CustomerID={account.CustomerID}",
                "-Transactions-",
                transactionStrings
            }).ToString();
        }
    }
}
