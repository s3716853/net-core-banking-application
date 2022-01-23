using System.Text;
using MCBABackend.Models;
using MCBABackend.Models.Dto;

namespace MCBABackend.Utilities.Extensions;
public static class TransactionExtension
{
    // Cannot override ToString with extension methods
    // Chose to use an Extension method instead of implementing an overrided ToString
    // in the model classes as to keep them purely as data classes
    public static string AsString(this Transaction transaction)
    {
        return new StringBuilder().AppendArray(new string[]
        {
            $"      TransactionId={transaction.TransactionID}",
            $"      TransactionType={transaction.TransactionType}",
            $"      OriginAccountNumber={transaction.OriginAccountNumber}",
            $"      DestinationAccountNumber={transaction.DestinationAccountNumber}",
            $"      Amount={transaction.Amount}",
            $"      Comment={transaction.Comment}",
            $"      TransactionTimeUtc={transaction.TransactionTimeUtc.ToLocalTime()}",
        }).ToString();
    }

    public static Transaction ToTransaction(this TransactionDto dto, string accountNumber)
    {
        return new Transaction()
        {
            TransactionID = default,
            TransactionType = dto.TransactionType,
            OriginAccountNumber = accountNumber,
            Amount = dto.Amount,
            Comment = dto.Comment,
            TransactionTimeUtc = dto.TransactionTimeUtc
        };
    }
}
